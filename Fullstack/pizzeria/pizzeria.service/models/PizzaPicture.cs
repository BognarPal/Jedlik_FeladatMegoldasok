using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pizzeria.service.models
{
    public class PizzaPicture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Pizza Pizza { get; set; }
        [Required]
        public byte[] Picture { get; set; }
    }
}
