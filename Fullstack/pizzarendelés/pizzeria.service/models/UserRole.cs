using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
