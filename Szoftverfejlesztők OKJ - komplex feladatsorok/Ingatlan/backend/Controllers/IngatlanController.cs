using IngatlanWebAPI.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngatlanWebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class IngatlanController : ControllerBase
    {
        IngatlanContext context;

        public IngatlanController(IngatlanContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("kategoriak")]
        public IEnumerable<Kategoria> KategoriaLista()
        {
            return context.Set<Kategoria>().Select(k => k).ToList();
        }

        [HttpPost]
        [Route("ujingatlan")]
        public ObjectResult UjIngatlan(Ingatlan ingatlan)
        {
            context.Set<Ingatlan>().Add(ingatlan);
            context.SaveChanges();
            return Ok(context.Set<Ingatlan>().Include(i => i.Kategoria).Select(i => new
            {
                i.Id,
                i.KategoriaId,
                KategoriaNev = i.Kategoria.Megnevezes,
                i.Leiras,
                HirdetesDatuma = i.HirdetesDatuma.ToString("yyyy.MM.dd"),
                i.Tehermentes,
                i.kepUrl
            }).First(i => i.Id == ingatlan.Id));
        }

        [HttpGet]
        [Route("ingatlan")]
        public ObjectResult IngatlanLista()
        {
            return Ok(context.Set<Ingatlan>().Include(i => i.Kategoria).Select( i => new
            {
                i.Id,
                i.KategoriaId,
                KategoriaNev = i.Kategoria.Megnevezes,
                i.Leiras,
                HirdetesDatuma = i.HirdetesDatuma.ToString("yyyy.MM.dd"),
                i.Tehermentes,
                i.kepUrl
            }).ToList());
        }
    }
}
