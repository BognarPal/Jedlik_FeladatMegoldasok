using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace pizzeria.service.models
{
    public class Order : IOrder
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public ICustomer Customer { get; set; }
        public DateTime OrderTimeStamp { get; set; }
        public DateTime? CookingStartTimeStamp { get; set; }
        public DateTime? DeliveringTimeStamp { get; set; }
        public DateTime? DeliveredTimeStamp { get; set; }
        [NotMapped]
        public Dictionary<IPizza, int> Pizzas { get; set; }
        [StringLength(1500)]
        public string CustomerComment { get; set; }
        [NotMapped]
        public IAddress Address { get; set; }

    }    
}
