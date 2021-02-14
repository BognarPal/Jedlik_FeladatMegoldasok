using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Munkalap.DTO.Authentication
{
    [Table("munkalap_sessions")]
    public class SessionModel
    {
        public int Id { get; set; }
        [StringLength(120)]
        [Unique]
        [Required]
        public string SessionID { get; set; }
        [StringLength(100)]
        [Required]
        public string AdLoginName { get; set; }
        [StringLength(60)]
        [Required]
        public string IpAddress { get; set; }
        [StringLength(100)]
        [Required]
        public string UserName { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
