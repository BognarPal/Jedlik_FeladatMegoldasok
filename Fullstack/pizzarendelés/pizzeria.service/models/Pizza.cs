using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class Pizza : IPizza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1500)]
        public string Description { get; set; }
        public IEnumerable<byte[]> Pictures { get; set; }

        private IEnumerable<PizzaTag> tags;

        public IEnumerable<PizzaTag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        IEnumerable<IPizzaTag> IPizza.Tags
        {
            get { return tags; }
            set { tags = value.Select(v => (PizzaTag)v); }
        }

        private IEnumerable<PizzaPrice> prices;

        public IEnumerable<PizzaPrice> Prices
        {
            get { return prices; }
            set { prices = value; }
        }

        IEnumerable<IPizzaPrice> IPizza.Prices
        {
            get { return prices; }
            set { prices = value.Select(v => (PizzaPrice)v); }
        }

    }
}
