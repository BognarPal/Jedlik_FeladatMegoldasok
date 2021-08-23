using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class Role : IRole
    {
        [Key]
        public int Id { get; set; }
        [Required]  
        [StringLength(30)]
        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
