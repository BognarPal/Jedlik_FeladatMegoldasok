using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Base
{
    [Table("munkalap_employeeProfessions")]
    public class EmployeeProfession
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public int ProfessionId { get; set; }
        public Profession Profession { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string ProfessionName { get; set; }
    }
}
