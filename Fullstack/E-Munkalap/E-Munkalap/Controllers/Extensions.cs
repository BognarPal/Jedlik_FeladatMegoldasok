using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace E_Munkalap.Controllers
{
    public static class Extensions
    {
        public static IActionResult RunWithErrorHandling(this ControllerBase controller, Func<IActionResult> method)
        {
            try
            {
                return method();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
#if DEBUG               
                return controller.StatusCode(500, new { error = ex.Message });
#else
                if (ex.SqlState == "munkalap")
                    return controller.StatusCode(500, new { error = ex.Message });
                return controller.StatusCode(500, new { error = "Hiba az adatbázis kommunikáció során" });
#endif
            }
            catch (UnauthorizedAccessException ex)
            {
                return controller.StatusCode(401, new { error = ex.Message });
            }
#if DEBUG
            catch (Exception ex)
#else
            catch
#endif
            {
#if DEBUG
                Console.WriteLine(ex.StackTrace);
                return controller.StatusCode(500, new { error = ex.Message });
#else
                return controller.StatusCode(500, new { error = "Váratlan hiba" });
#endif
            }
        }
    }
}
