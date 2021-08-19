using pizzeria.service.models;
using System;

namespace pizzeria.service.repositories
{
    public class PizzaTagRepository: GenericRepository<PizzaTag>
    {
        public PizzaTagRepository(ApplicationDbContext dbContext): base(dbContext)
        {}
    }
}