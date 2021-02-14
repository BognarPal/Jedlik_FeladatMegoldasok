using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;

namespace E_Munkalap.SQL
{
    public static class DatabaseMapper
    {
        public static List<T> Query<T>(this DatabaseProvider provider, string scriptName, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var con = provider.GetConnection())
            using (var tran = con.BeginTransaction())
            {
                try
                {
                    var result = con.Query<T>(sqlText(scriptName, param), param, tran, true, commandTimeout, commandType).ToList();
                    tran.Commit();
                    return result;
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw;
                }
            }
        }

        public static (List<T1>, List<T2>) Query<T1, T2>(this DatabaseProvider provider, string scriptName, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var con = provider.GetConnection())
            using (var tran = con.BeginTransaction())
            {
                try
                {
                    using (var result = con.QueryMultiple(sqlText(scriptName, param), param, tran, commandTimeout, commandType))
                    {
                        var returnValue = (result.Read<T1>().ToList(), result.Read<T2>().ToList());
                        tran.Commit();
                        return returnValue;
                    }
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw;
                }
            }
        }

        public static T QuerySingle<T>(this DatabaseProvider provider, string scriptName, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var con = provider.GetConnection())
            using (var tran = con.BeginTransaction())
            {
                try
                {
                    var result = con.QuerySingle<T>(sqlText(scriptName, param), param, tran, commandTimeout, CommandType.Text);
                    tran.Commit();
                    return result;
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw;
                }
            }
        }

        public static int Execute(this DatabaseProvider provider, string scriptName, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var con = provider.GetConnection())
            using (var tran = con.BeginTransaction())
            {
                try
                {
                    var result = con.Execute(sqlText(scriptName, param), param, tran, commandTimeout, commandType);
                    tran.Commit();
                    return result;
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch { }
                    throw;
                }
            }
        }

        private static string sqlText(string scriptName, object param)
        {
            List<string> lines = new List<string>();
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("E_Munkalap.SQL.scripts.{0}.sql", scriptName)))
            using (StreamReader reader = new StreamReader(stream))
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());

            List<string> sqlScripLines = new List<string>();

            Stack<bool> needCurrentSection = new Stack<bool>();
            needCurrentSection.Push(true);

            foreach (var line in lines.Select(l => l.Trim()))
                if (line.ToLower().StartsWith("--#include"))
                    sqlScripLines.Add(sqlText(line.Substring(11), param));
                else if (line.ToLower().StartsWith("--#endif"))
                    needCurrentSection.Pop();
                else if (line.ToLower().StartsWith("--#else"))
                    needCurrentSection.Push(!needCurrentSection.Pop() && needCurrentSection.Peek());
                else if (line.ToLower().StartsWith("--#if"))
                    needCurrentSection.Push(!line.Substring(5)
                                                 .Split(' ')
                                                 .Where(s => s.StartsWith("@"))
                                                 .Any(s => param == null || param.GetType().GetProperty(s.Substring(1)) == null || param.GetType().GetProperty(s.Substring(1)).GetValue(param) == null)
                                             && needCurrentSection.Peek()
                                           );
                else if (needCurrentSection.Peek())
                    sqlScripLines.Add(line);

            return string.Join("\n", sqlScripLines.ToArray());
        }
    }
}
