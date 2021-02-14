using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Base
{
    [Table("munkalap_professions")]
    public class Profession
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        [Unique]
        public string Name { get; set; }
    }
}
