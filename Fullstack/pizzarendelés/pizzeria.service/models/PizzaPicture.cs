using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class PizzaPicture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public byte[] Picture { get; set; }
    }
}