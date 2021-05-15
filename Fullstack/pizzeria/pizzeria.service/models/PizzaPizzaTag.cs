using pizzeria.data.interfaces.models;
using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class PizzaPizzaTag: IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Pizza Pizza { get; set; }
        [Required]
        public PizzaTag PizzaTag { get; set; }
    }
}
