using pizzeria.data.interfaces.models;
using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class PizzaTag : IPizzaTag
    {
        [Key]
        public int Id { get; set ; }
        [StringLength(50)]
        [Required]
        public string Name { get;  set; }
    }
}
