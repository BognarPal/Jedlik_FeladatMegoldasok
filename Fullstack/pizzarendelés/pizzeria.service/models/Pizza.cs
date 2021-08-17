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

        public List<PizzaPicture> Pictures { get; set; }
        IEnumerable<byte[]> IPizza.Pictures 
        {
            get => Pictures.Select(p => p.Picture).ToArray();
            set => Pictures = value.Select(v => new PizzaPicture() { Id = 0, Picture = v }).ToList();
        }

        public List<PizzaTag> Tags { get; set; }
        IEnumerable<IPizzaTag> IPizza.Tags
        {
            get => Tags;
            set => Tags = value.Select(v => (PizzaTag)v).ToList();
        }

        public List<PizzaPrice> Prices { get; set; }
        IEnumerable<IPizzaPrice> IPizza.Prices
        {
            get => Prices;
            set => Prices = value.Select(v => (PizzaPrice)v).ToList();
        }

        public List<PizzaPizzaTag> PizzaPizzaTags { get; set; }
    }
}
