using pizzeria.data.interfaces.models;
using System.ComponentModel.DataAnnotations;

namespace pizzeria.service.models
{
    public class UserRole: IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
