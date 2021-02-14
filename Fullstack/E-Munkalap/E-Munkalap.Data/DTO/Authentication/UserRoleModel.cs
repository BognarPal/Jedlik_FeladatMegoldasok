using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Authentication
{
    [Table("munkalap_userRoles")]
    public class UserRoleModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string AdLoginName { get; set; }
        [Required]
        public RoleModel Role { get; set; }        
    }
}
