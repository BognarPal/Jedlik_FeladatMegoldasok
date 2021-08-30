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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository<Order>
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override Order GetById(int id)
        {
            var order = dbContext.Set<Order>()
                                 .Include(o => o.Address)
                                 .Include(o => o.Customer)
                                 .Include(o => o.OrderDetails).ThenInclude(d => d.Pizza)
                                 .FirstOrDefault(o => o.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return order;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public override IEnumerable<Order> GetAll()
        {
            throw new NotSupportedException();
        }

        public override Order Add(Order entity)
        {
            entity.OrderTimeStamp = DateTime.Now;
            return base.Add(entity);
        }

        public override IEnumerable<Order> AddRange(IEnumerable<Order> entities)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Order entity)
        {
            throw new NotSupportedException();
        }

        public override void RemoveRange(IEnumerable<Order> entities)
        {
            throw new NotSupportedException();
        }

        public override Order Update(Order entity)
        {
            throw new NotSupportedException();
        }

        public override IEnumerable<Order> Search(Expression<Func<Order, bool>> predicate)
        {
            return dbContext.Set<Order>()
                            .Include(o => o.Address)
                            .Include(o => o.Customer)
                            .Include(o => o.OrderDetails).ThenInclude(d => d.Pizza)
                            .Where(predicate);
        }

        public IEnumerable<Order> OrdersToBeCooked()
        {
            return Search(o => o.CookingStartTimeStamp == null);
        }

        public void StartCooking(Order order)
        {
            order.CookingStartTimeStamp = DateTime.Now;
            base.Update(order);
        }

        public void StartOfDelivery(Order order)
        {
            order.DeliveringTimeStamp = DateTime.Now;
            base.Update(order);
        }

        public void Delivered(Order order)
        {
            order.DeliveredTimeStamp = DateTime.Now;
            base.Update(order);
        }
    }
}
