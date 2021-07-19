using System;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.models
{
    public interface IOrder : IEntity
    {
        ICustomer Customer { get; set; }
        DateTime OrderTimeStamp { get; set; }
        DateTime? CookingStartTimeStamp { get; set; }
        DateTime? DeliveringTimeStamp { get; set; }
        DateTime? DeliveredTimeStamp { get; set; }
        IDictionary<IPizza, int> Pizzas { get; set; }
        string CustomerComment { get; set; }
        IAddress Address { get; set; }
    }
}
