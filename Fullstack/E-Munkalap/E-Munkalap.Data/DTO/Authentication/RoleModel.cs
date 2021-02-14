using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Authentication
{
    [Table("munkalap_roles")]
    public class RoleModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        [Unique]
        public string Name { get; set; }
    }
}
