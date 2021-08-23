using Microsoft.EntityFrameworkCore;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository<Employee>
    {
        public override Employee GetById(int id)
        {
            var employee = dbContext.Set<Employee>()
                                    .Include(e => e.UserRoles).ThenInclude(r => r.Role)
                                    .FirstOrDefault(p => p.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return employee;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override IEnumerable<Employee> GetAll()
        {
            return dbContext.Set<Employee>()
                            .Include(e => e.UserRoles).ThenInclude(r => r.Role)
                            .ToList();
        }

        public override IEnumerable<Employee> Search(Expression<Func<Employee, bool>> predicate)
        {
            return dbContext.Set<Employee>()
                            .Include(e => e.UserRoles).ThenInclude(r => r.Role)
                            .Where(predicate)
                            .ToList();
        }

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override IEnumerable<Employee> AddRange(IEnumerable<Employee> entities)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRange(IEnumerable<Employee> entities)
        {
            throw new NotSupportedException();
        }
    }
}
