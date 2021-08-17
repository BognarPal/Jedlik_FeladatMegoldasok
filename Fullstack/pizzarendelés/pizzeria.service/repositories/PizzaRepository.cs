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
                                 .Include(p => p.PizzaPizzaTags).ThenInclude(t => t.PizzaTag)
                                 .Where(p => p.Id == id)
                                 .FirstOrDefault();
#pragma warning disable CS8603 // Possible null reference return.
            return pizza;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override IEnumerable<Pizza> GetAll()
        {
            return dbContext.Set<Pizza>()
                            .Include(p => p.Pictures)
                            .Include(p => p.Prices)
                            .Include(p => p.PizzaPizzaTags).ThenInclude(t => t.PizzaTag)
                            .ToList();
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
            var originalPizza = dbContext.Set<Pizza>()
                                         .Include(p => p.Prices)
                                         .FirstOrDefault(p => p.Id == entity.Id);
            if (originalPizza == null)
                throw new Exception("A pizza nem létezik");

            foreach (var price in entity.Prices)
            {
                if (!originalPizza.Prices.Contains(price))
                    throw new NotSupportedException("A pizza módosítása során az ár módosítása nem támogatott");
            }
            if (originalPizza.Prices.Count != entity.Prices.Count)
                throw new NotSupportedException("A pizza módosítása során az ár módosítása nem támogatott");

            return base.Update(entity);
        }

        public override IEnumerable<Pizza> Search(Expression<Func<Pizza, bool>> predicate)
        {
            return dbContext.Set<Pizza>()
                            .Include(p => p.Pictures)
                            .Include(p => p.Prices)
                            .Include(p => p.PizzaPizzaTags).ThenInclude(t => t.PizzaTag)
                            .Where(predicate)
                            .ToList();
        }

        public IEnumerable<Pizza> GetByTags(IEnumerable<string> tagNames)
        {
            var pizzaTags = dbContext.Set<PizzaTag>()
                                     .Where(t => tagNames.Any(n => n == t.Name));

            return Search(p => p.PizzaPizzaTags.Select(t => t.PizzaTag).Intersect(pizzaTags).Count() == pizzaTags.Count());
        }

        public Pizza RemoveLastPrice(int pizzaId)
        {
            var pizza = GetById(pizzaId);
            if (pizza == null || pizza.Prices == null)
                throw new InvalidDataException();
            return RemoveLastPrice(pizza);
        }

        public Pizza RemoveLastPrice(Pizza pizza)
        {
            if (pizza == null)
                throw new ArgumentNullException();

            if (pizza.Prices == null)
                return RemoveLastPrice(pizza.Id);

            if (pizza.Prices.Count < 2)
                throw new Exception("Az utolsó ár nem törölhető");

            if (pizza.Prices.Where(p => p.ToDate == null).Count() != 1)
                throw new Exception("Az utolsó ár nem meghatározható");

            var lastPrice = pizza.Prices.First(p => p.ToDate == null);

            if (pizza.Prices.Where(p => p.ToDate == lastPrice.FromDate.AddDays(-1)).Count() != 1)
                throw new Exception("Az utolsó előtti ár nem meghatározható");

            var newLastPrice = pizza.Prices.First(p => p.ToDate == lastPrice.FromDate.AddDays(-1));
            
            dbContext.Set<PizzaPrice>().Remove(lastPrice);

            newLastPrice.ToDate = null;
            dbContext.Entry(newLastPrice).State = EntityState.Modified;

            return pizza;
        }

        public Pizza UpdatePrice(int pizzaId, DateTime fromDate, decimal newPrice)
        {
            var pizza = GetById(pizzaId);
            if (pizza == null || pizza.Prices == null)
                throw new InvalidDataException();
            return UpdatePrice(pizza, fromDate, newPrice);
        }

        private Pizza UpdatePrice(Pizza pizza, DateTime fromDate, decimal newPrice)
        {
            if (pizza == null)
                throw new ArgumentNullException();

            if (pizza.Prices == null)
                return UpdatePrice(pizza.Id, fromDate, newPrice);

            if (pizza.Prices.Count() != 0)
            {
                var lastFromDate = pizza.Prices.Where(p => p.ToDate == null).Max(p => p.FromDate);
                if (lastFromDate >= fromDate)
                    throw new ArgumentException($"A {fromDate:yyyy.MM.dd}-ai dátumnál későbbi ár már szerepel a nyilvántartásban");

                foreach (var price in pizza.Prices.Where(p => p.ToDate == null))
                {
                    price.ToDate = fromDate.AddDays(-1);
                    dbContext.Entry(price).State = EntityState.Modified;
                }
            }
            pizza.Prices.Add(new PizzaPrice() { FromDate = fromDate, ToDate = null, Price = newPrice });

            return pizza;
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

            var currentPizzaPrice = pizza.Prices.Where(p => p.FromDate <= DateTime.Today).FirstOrDefault(p => p.ToDate == null || p.ToDate > DateTime.Today);

            return currentPizzaPrice == null ? null : currentPizzaPrice.Price;
        }
    }
}