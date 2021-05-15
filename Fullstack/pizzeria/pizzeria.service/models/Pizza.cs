using pizzeria.data.interfaces.models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace pizzeria.service.models
{
    public class Pizza: IPizza
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [StringLength(1500)]
        public string Description { get; set; }
        [NotMapped]
        public List<byte[]> Pictures { get; set; }
        [NotMapped]
        public List<IPizzaTag> Tags { get; set; }
    }
}
