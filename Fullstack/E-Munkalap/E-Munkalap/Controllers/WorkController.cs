using E_Munkalap.DTO.Work;
using E_Munkalap.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Munkalap.Controllers
{
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly DatabaseProvider databaseProvider;

        public WorkController(IOptions<DatabaseProvider> dbProvider)
        {
            databaseProvider = dbProvider.Value;
        }

        [HttpGet]
        [Route("works/list")]
        public IActionResult GetEmployeeProfession()
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Work>("work.works_select")
                );
            });
        }

        [HttpPost]
        [Route("works/new")]
        public IActionResult NewWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                return Ok
                (
                    databaseProvider.Query<Work>("work.work_insert", work).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("works/assign")]
        public IActionResult AssignWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("work.work_assign", work);
                return Ok
                (
                    databaseProvider.Query<Work>("work.works_select", new { id = work.Id }).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("works/finish")]
        public IActionResult FinishWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("work.work_finish", work);
                return Ok
                (
                    databaseProvider.Query<Work>("work.works_select", new { work.Id }).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("works/check")]
        public IActionResult CheckWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("work.work_check", work);
                return Ok
                (
                    databaseProvider.Query<Work>("work.works_select", new { work.Id }).FirstOrDefault()
                );
            });
        }

        [HttpPut]
        [Route("works/update")]
        public IActionResult UpdateWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("work.work_update", work);
                return Ok
                (
                    databaseProvider.Query<Work>("work.works_select", new { work.Id }).FirstOrDefault()
                );
            });
        }

        [HttpDelete]
        [Route("works/delete")]
        public IActionResult DeleteWork(Work work)
        {
            return this.RunWithErrorHandling(() =>
            {
                databaseProvider.Execute("work.work_delete", work);
                return Ok(true);
            });
        }
    }
}
