using Microsoft.EntityFrameworkCore;
using pizzeria.data.interfaces.models;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace pizzeria.service.repositories
{
    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository<Pizza>
    {
        public PizzaRepository(ApplicationDbContext dbContext): base(dbContext)
        {}

        public override Pizza GetById(int id)
        {
            var pizza = dbContext.Set<Pizza>()
                                  .Include(p => p.Pictures)
                                  .Include(p => p.Prices)
                                  .Include(p => p.PizzaPizzaTags).ThenInclude(p => p.PizzaTag)
                                  .FirstOrDefault(p => p.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return pizza;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public decimal? CurrentPrice(int pizzaId)
        {
            throw new NotImplementedException();
        }

        public decimal? CurrentPrice(IPizza pizza)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pizza> GetByTags(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public Pizza RemoveLastPrice(int pizzaId)
        {
            throw new NotImplementedException();
        }

        public Pizza UpdatePrice(int pizzaId, DateTime fromDate, decimal newPrice)
        {
            throw new NotImplementedException();
        }
    }
}