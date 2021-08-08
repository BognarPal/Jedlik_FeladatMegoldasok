using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class Order: IOrder
    {
        [Key]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        ICustomer IOrder.Customer
        {
            get => Customer; 
            set => Customer = (Customer)value; 
        }

        public DateTime OrderTimeStamp { get; set; }
        public DateTime? CookingStartTimeStamp { get; set; }
        public DateTime? DeliveringTimeStamp { get; set; }
        public DateTime? DeliveredTimeStamp { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        IDictionary<IPizza, int> IOrder.Pizzas
        {
            get => OrderDetails.ToDictionary(v => (IPizza)v.Pizza, v => v.Count);
            set => OrderDetails = value.Select(v => new OrderDetails() { Id = 0, Pizza = (Pizza)v.Key, Count = v.Value });
        }

        [StringLength(1500)]
        public string CustomerComment { get; set; }

        public Address Address { get; set; }
        IAddress IOrder.Address
        {
            get => Address;
            set => Address = (Address)value;
        }

    }
}
