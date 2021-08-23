using pizzeria.data.interfaces.models;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.operations
{
    public interface IOrderRepository<T> : IGenericRepository<T> where T : class, IOrder
    {
        IEnumerable<T> OrdersToBeCooked();
        void StartCooking(T order);
        void StartOfDelivery(T order);
        void Delivered(T order);
    }
}
