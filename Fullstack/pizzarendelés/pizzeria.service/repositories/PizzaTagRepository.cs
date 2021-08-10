using pizzeria.service.models;
using System;

namespace pizzeria.service.repositories
{
    public class PizzaTagRepository: GenericRepository<PizzaTag>
    {
        private ApplicationDbContext dbContext;

        public PizzaTagRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}