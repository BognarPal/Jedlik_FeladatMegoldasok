using pizzeria.data.interfaces.models;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.operations
{
    public interface IOrderRepository : IGenericRepository<IOrder>
    {
        IEnumerable<IOrder> OrdersToBeCooked();
        void StartCooking(IOrder order);
        void StartOfDelivery(IOrder order);
        void Delivered(IOrder order);
    }
}
