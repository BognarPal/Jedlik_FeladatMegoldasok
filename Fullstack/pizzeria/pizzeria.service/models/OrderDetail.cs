using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pizzeria.service.models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Order Order { get; set; }
        [Required]
        public Pizza Pizza { get; set; }
        public int Count { get; set; }
    }
}
