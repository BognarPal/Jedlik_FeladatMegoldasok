using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    [Table("User")]
    public class User : IUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [NotMapped]
        public IEnumerable<Role> Roles
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => UserRoles == null ? null : UserRoles.Select(u => u.Role);
#pragma warning restore CS8603 // Possible null reference return.
        }
        IEnumerable<IRole> IUser.Roles
        {
            get => Roles;
            set => throw new NotSupportedException();
        }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
