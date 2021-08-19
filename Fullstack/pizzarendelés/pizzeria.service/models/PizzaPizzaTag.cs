using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class PizzaPizzaTag
    {
        [Key]
        public int Id {  get; set; }
        public Pizza Pizza { get; set; }
        public PizzaTag PizzaTag { get; set; }
    }
}
