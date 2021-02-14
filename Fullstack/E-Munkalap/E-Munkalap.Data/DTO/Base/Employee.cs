using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Base
{
    [Table("munkalap_employees")]
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Unique]
        public string Name { get; set; }

        [StringLength(100)]
        public string AdLoginName { get; set; }        

    }
}
