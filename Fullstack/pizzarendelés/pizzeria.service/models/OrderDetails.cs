using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Pizza Pizza { get; set; }
        public int Count { get; set; }
    }
}