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

        private IDictionary<Pizza, int> pizzas;

        public IDictionary<Pizza, int> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; }
        }

        IDictionary<IPizza, int> IOrder.Pizzas
        {
            get { return pizzas.ToDictionary(v => (IPizza)v.Key, v => v.Value); }
            set { pizzas = value.ToDictionary(v => (Pizza)v.Key, v => v.Value); }
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
