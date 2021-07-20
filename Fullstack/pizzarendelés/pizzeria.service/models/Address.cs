using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
