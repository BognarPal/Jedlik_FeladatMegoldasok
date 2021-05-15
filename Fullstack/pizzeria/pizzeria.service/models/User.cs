using pizzeria.data.interfaces.models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace pizzeria.service.models
{
    public abstract class User : IUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(300)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }
        [NotMapped]
        public List<IRole> Roles { get; set; }
    }
}
