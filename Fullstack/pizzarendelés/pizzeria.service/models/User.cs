using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
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

        private IEnumerable<Role> roles;

        public IEnumerable<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        IEnumerable<IRole> IUser.Roles
        {
            get { return roles; }
            set { roles = value.Select(v => (Role)v); }
        }

    }
}
