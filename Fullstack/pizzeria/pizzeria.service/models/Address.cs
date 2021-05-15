using pizzeria.data.interfaces.models;
using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class Address : IAddress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string StreetAndHouseNumber { get; set; }
        [StringLength(100)]
        public string FloorAndDoor { get; set; }
        [StringLength(500)]
        public string Comment { get; set; }
    }
}
