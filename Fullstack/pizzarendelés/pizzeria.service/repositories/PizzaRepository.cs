using Microsoft.EntityFrameworkCore;
using pizzeria.data.interfaces.models;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pizzeria.service.repositories
{
    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository<Pizza>
    {
        public PizzaRepository(ApplicationDbContext dbContext): base(dbContext)
        {}

        public override Pizza GetById(int id)
        {
            //var pizza = base.GetById(id);
            var pizza = dbContext.Set<Pizza>()
                                 .Include(p => p.Pictures)
                                 .Include(p => p.Prices )
                                 .Where(p => p.Id == id)
                                 .FirstOrDefault();
            if (pizza != null)
            {
                pizza.Tags = dbContext.Set<PizzaTag>()
                                      .Where(t => t.Pizzas.Contains(pizza));
                
            }
#pragma warning disable CS8603 // Possible null reference return.
            return pizza;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<Pizza> GetByTags(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }

        public decimal? GetPrice(int pizzaId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPizzaPrice> GetPrices(int pizzaId)
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