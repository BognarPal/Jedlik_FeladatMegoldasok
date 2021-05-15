using pizzeria.data.interfaces.models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace pizzeria.service.models
{
    public class Customer : User, ICustomer
    {
        [NotMapped]
        public IAddress PrimaryAddress { get; set; }
        [NotMapped]
        public List<IAddress> Addresses { get; set; }
        [NotMapped]
        public List<IOrder> PreviousOrders { get; set; }

        public Customer()
        {
            Roles = new List<IRole> { new Role() { Name = "customer" } };
        }

    }
}
