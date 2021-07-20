using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.repositories
{
    public class PizzaTagRepository : GenericRepository<PizzaTag>
    {
        public PizzaTagRepository(ApplicationDbContext dbContext): base(dbContext)
        { }

    }
}
