using DinkToPdf;
using DinkToPdf.Contracts;
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
        private readonly IConverter converter;

        public WorkController(IOptions<DatabaseProvider> dbProvider, IConverter converter)
        {
            databaseProvider = dbProvider.Value;
            this.converter = converter;
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
                    databaseProvider.Query<Work>("work.works_select", new { id = work.Id }).FirstOrDefault()
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
                    databaseProvider.Query<Work>("work.works_select", new { id = work.Id }).FirstOrDefault()
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
                    databaseProvider.Query<Work>("work.works_select", new { id = work.Id }).FirstOrDefault()
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

        [HttpGet]
        [Route("works/pdf/{id}")]
        public IActionResult GetPdf(int id)
        {
            return this.RunWithErrorHandling(() =>
            {
                var work = databaseProvider.Query<Work>("work.works_select", new { id }).FirstOrDefault();
                if (work != null)
                {
                    return File(createReport(work), "application/pdf", $"munkalap_{work.Id}.pdf");
                }
                return NotFound("Nem létező azonosító!");
            });
        }

        private byte[] createReport(Work work)
        {
            var html = "<html lang = \"hu\">" +
                       "<head>" +
                       "   <meta charset = \"utf-8\" >" +
                       "   <style> " +
                       "      body {" +
                       "         font-size: 16pt;" +
                       "      }" +
                       "      h1 {" +
                       "         text-align: center;" +
                       "         font-size: 1.5rem;" +
                       "         margin-top: 4rem;" +
                       "         margin-bottom: 2rem;" +
                       "      }" +
                       "      .alcim {" +
                       "         text-align: center;" +
                       "         font-size: 1.2rem;" +
                       "         font-style: italic;" +
                       "      }" +
                       "      fieldset {" +
                       "          width: 50rem;" +
                       "          margin: 0 auto;" +
                       "          padding: 0rem 2rem;" +
                       "      }" +
                       "      fieldset p {" +
                       "          font-weight: normal;" +
                       "          padding-left: 10rem;" +
                       "      }" +
                       "      .label {" +
                       "          font-weight: bold;" +
                       "          display: inline-block;" +
                       "          margin-left: -10rem;" +
                       "          width: 9.75rem;" +
                       "      }" +
                       "      .alairas {" +
                       "          text-align: center;" +
                       "          margin-right: 2rem;" +
                       "          padding: 0 2rem;" +
                       "          float: right;" +
                       "          border-top: 2px dotted black;" +
                       "          min-width: 8rem;" +
                       "      }" +
                       "   </style> " +
                       "</head>" +
                       "<body>" +
                       "   <h1>Munkalap<br>" +
                      $"   <span class=\"alcim\">Azonosító: {work.Id}</span></h1>" +
                       "   <fieldset>" +
                       "       <legend>Bejelentő</legend>" +
                       "       <p>" +
                       "           <span class=\"label\">Neve:</span>" +
                      $"           {work.RequesterName}" +
                       "       </p>" +
                       "       <p>" +
                       "           <span class=\"label\">Időpont:</span>" +
                      $"           {work.RequestDate:yyyy.MM.dd HH:mm}" +
                       "       </p>" +
                       "       <p>" +
                       "           <span class=\"label\">Probléma:</span>" +
                      $"           {work.Description}" +
                       "       </p>" +
                       "   </fieldset>" +
                       "   <fieldset>" +
                       "       <legend>Feladat kiosztása</legend>" +
                       "       <p>" +
                       "           <span class=\"label\">Felelős:</span>" +
                      $"           {work.EmployeeName}" +
                       "       </p>" +
                       "       <p>" +
                       "           <span class=\"label\">Határidő:</span>" +
                      $"           {work.DeadLine:yyyy.MM.dd}" +
                       "       </p>" +
                       "       <p>" +
                       "           <span class=\"label\">Megjegyzés:</span>" +
                      $"           {work.AssignDetails}" +
                       "       </p>" +
                       "       <p class=\"alairas\">" +
                      $"           {work.AssignerName}" +
                       "       </p>" +
                       "   </fieldset>" +
                       "   <fieldset>" +
                       "       <legend>Feladat elvégzése</legend>" +
                       "       <p>" +
                       "           <span class=\"label\">Dátum:</span>" +
                      $"           {work.FinishDate:yyyy.MM.dd}" +
                       "       </p>" +
                       "       <p style=\"min-height: 5rem;\">" +
                       "           <span class=\"label\">Megjegyzés:</span>" +
                      $"           {work.FinishComment}" +
                       "       </p>" +
                       "       <p class=\"alairas\">" +
                      $"           {work.EmployeeName}" +
                       "       </p>" +
                       "   </fieldset>" +
                       "   <fieldset>" +
                       "       <legend>Ellenőrzés</legend>" +
                       "       <p>" +
                       "           <span class=\"label\">Dátum:</span>" +
                      $"           {work.CheckDate:yyyy.MM.dd}" +
                       "       </p>" +
                       "       <p style=\"min-height: 5rem;\">" +
                       "           <span class=\"label\">Megjegyzés:</span>" +
                      $"           {work.CheckComment}" +
                       "       </p>" +
                       "       <p class=\"alairas\" >" +
                      $"           &nbsp;{work.CheckerUser}&nbsp;" +
                       "       </p>" +
                       "   </fieldset>" +
                       "</body>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },

                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html
                    }
                }
            };

            return converter.Convert(doc);

        }
    }
}
