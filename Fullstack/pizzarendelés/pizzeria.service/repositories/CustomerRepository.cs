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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository<Customer>
    {
        public override Customer GetById(int id)
        {
            var customer = dbContext.Set<Customer>()
                                    .Include(c => c.Addresses)
                                    .Include(c => c.PrimaryAddress)
                                    .Include(c => c.UserRoles).ThenInclude(r => r.Role)
                                    .FirstOrDefault(p => p.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return customer;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override IEnumerable<Customer> GetAll()
        {
            return dbContext.Set<Customer>()
                            .Include(c => c.Addresses)
                            .Include(c => c.PrimaryAddress)
                            .Include(c => c.UserRoles).ThenInclude(r => r.Role)
                            .ToList();
        }

        public override IEnumerable<Customer> Search(Expression<Func<Customer, bool>> predicate)
        {
            return dbContext.Set<Customer>()
                            .Include(c => c.Addresses)
                            .Include(c => c.PrimaryAddress)
                            .Include(c => c.UserRoles).ThenInclude(r => r.Role)
                            .Where(predicate)
                            .ToList();
        }

        public override Customer Add(Customer entity)
        {
            if (!entity.Addresses.Contains(entity.PrimaryAddress))
            {
                var addresses = entity.Addresses.ToList();
                addresses.Add(entity.PrimaryAddress);
                entity.Addresses = addresses;
            }

            var customer = base.Add(entity);

            var customerRole = new GenericRepository<Role>(dbContext).Search(r => r.Name == "customer").FirstOrDefault();
            if (customerRole != null)
                customer.UserRoles = new List<UserRole>()
                {
                    new UserRole() { User = customer, Role = customerRole}
                };
            return customer;
        }

        public override IEnumerable<Customer> AddRange(IEnumerable<Customer> entities)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRange(IEnumerable<Customer> entities)
        {
            throw new NotSupportedException();
        }
    }
}
