using System;
using System.Collections.Generic;
using System.Text;

namespace pizzeria.data.interfaces.models
{
    public interface IOrder: IEntity
    {        
        ICustomer Customer { get; set; }
        DateTime OrderTimeStamp { get; set; }
        DateTime? CookingStartTimeStamp { get; set; }
        DateTime? DeliveringTimeStamp { get; set; }
        DateTime? DeliveredTimeStamp { get; set; }
        Dictionary<IPizza, int> Pizzas { get; set; }
        string CustomerComment { get; set; }
        IAddress Address { get; set; }
    }
}
