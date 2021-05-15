using pizzeria.data.interfaces.models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzeria.service.models
{
    public class PizzaPrice : IPizzaPrice
    {
        public int Id { get; set; }
        [NotMapped]
        public IPizza Pizza { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal Price { get; set; }
    }
}
