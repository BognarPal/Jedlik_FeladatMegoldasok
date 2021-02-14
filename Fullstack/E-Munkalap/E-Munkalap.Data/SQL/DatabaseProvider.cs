using E_Munkalap.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace E_Munkalap.SQL
{
    public class DatabaseProvider
    {
        public string Munkalap { get; set; }

        public IDbConnection GetConnection()
        {
            var connection = new MySqlConnection(Munkalap);
            connection.Open();

            return connection;
        }

        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MunkalapContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        do
                        {
                            Console.WriteLine("HIBA: " + ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                    }
                }
            }
        }

        public static void SetDbContext(IServiceCollection services, string constr)
        {
            services.AddDbContext<MunkalapContext>(options => options.UseMySQL(constr));

        }
    }
}
