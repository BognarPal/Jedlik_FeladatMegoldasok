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
            //var pizza = base.GetById(id);
            var pizza = dbContext.Set<Pizza>()
                                 .Include(p => p.Pictures)
                                 .Include(p => p.Prices )
                                 .Where(p => p.Id == id)
                                 .FirstOrDefault();
            if (pizza != null)
            {
                pizza.Tags = dbContext.Set<PizzaTag>()
                                      .Where(t => t.Pizzas.Contains(pizza))
                                      .ToList();
                
            }
#pragma warning disable CS8603 // Possible null reference return.
            return pizza;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override IEnumerable<Pizza> GetAll()
        {
            var pizzas = dbContext.Set<Pizza>()
                                  .Include(p => p.Pictures)
                                  .Include(p => p.Prices)
                                  .ToList();
            var tags = dbContext.Set<PizzaTag>().ToList();
            foreach (var pizza in pizzas)
            {
                pizza.Tags = dbContext.Set<PizzaTag>()
                                      .Where(t => t.Pizzas.Contains(pizza))
                                      .ToList();
            }
            return pizzas;
        }

        public override IEnumerable<Pizza> AddRange(IEnumerable<Pizza> entities)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRange(IEnumerable<Pizza> entities)
        {
            throw new NotSupportedException();
        }

        public override Pizza Update(Pizza entity)
        {
            var pizza = base.Update(entity);
            var removedTags = dbContext.Set<PizzaTag>()
                                       .Where(t => t.Pizzas.Contains(pizza) && !pizza.Tags.Select(i => i.Id).Contains(t.Id))
                                       .Include(t => t.Pizzas);
            foreach (var removedTag in removedTags)
                removedTag.Pizzas.Remove(pizza);

            return pizza;
        }

        public override IEnumerable<Pizza> Search(Expression<Func<Pizza, bool>> predicate)
        {
            var pizzas = dbContext.Set<Pizza>()
                                  .Include(p => p.Pictures)
                                  .Include(p => p.Prices)
                                  .Where(predicate)
                                  .ToList();

            var tags = dbContext.Set<PizzaTag>().ToList();
            foreach (var pizza in pizzas)
            {
                pizza.Tags = dbContext.Set<PizzaTag>()
                                      .Where(t => t.Pizzas.Contains(pizza))
                                      .ToList();
            }
            return pizzas;
        }

        public IEnumerable<Pizza> GetByTags(IEnumerable<string> tagNames)
        {
            var pizzaTags = dbContext.Set<PizzaTag>()
                                     .Where(t => tagNames.Any(n => n == t.Name));
            return Search(p => p.Tags.Intersect(pizzaTags).Count() == pizzaTags.Count());
        }

        public Pizza RemoveLastPrice(int pizzaId)
        {
            throw new NotImplementedException();
        }

        public Pizza UpdatePrice(int pizzaId, DateTime fromDate, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public decimal? CurrentPrice(int pizzaId)
        {
            var pizza = dbContext.Set<Pizza>()
                                 .Include(p => p.Prices)
                                 .FirstOrDefault(p => p.Id == pizzaId);

            return pizza == null || pizza.Prices == null ? null : CurrentPrice(pizza);
        }

        public decimal? CurrentPrice(IPizza pizza)
        {
            if (pizza == null)
                return null;

            if (pizza.Prices == null)
                return CurrentPrice(pizza.Id);

            var currentPizzaPrice = pizza.Prices.FirstOrDefault(p => p.ToDate == null);

            return currentPizzaPrice == null ? null : currentPizzaPrice.Price;
        }
    }
}