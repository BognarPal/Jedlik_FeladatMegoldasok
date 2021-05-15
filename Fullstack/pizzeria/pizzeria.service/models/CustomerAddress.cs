using pizzeria.data.interfaces.models;
using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class CustomerAddress: IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Address Address { get; set; }
        public bool IsPrimary { get; set; }
    }
}
