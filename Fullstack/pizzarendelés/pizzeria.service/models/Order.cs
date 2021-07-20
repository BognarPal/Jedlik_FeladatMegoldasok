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
        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        ICustomer IOrder.Customer
        {
            get { return customer; }
            set { customer = (Customer)value; }
        }

        public DateTime OrderTimeStamp { get; set; }
        public DateTime? CookingStartTimeStamp { get; set; }
        public DateTime? DeliveringTimeStamp { get; set; }
        public DateTime? DeliveredTimeStamp { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        IDictionary<IPizza, int> IOrder.Pizzas
        {
            get { return OrderDetails.ToDictionary(v => (IPizza)v.Pizza, v => v.Count); }
            set { throw new NotImplementedException(); }
        }

        [StringLength(1500)]
        public string CustomerComment { get; set; }

        private Address address;

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        IAddress IOrder.Address
        {
            get { return address; }
            set { address = (Address)value; }
        }

    }
}
