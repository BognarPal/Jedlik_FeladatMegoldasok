using E_Munkalap.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Munkalap.DTO.Work
{
    [Table("munkalap_works")]
    public class Work
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        //[Required]
        public string RequesterName { get; set; }
        //[Required]
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; }
        public int? ProfessionId { get; set; }
        public Profession Profession { get; set; }
        [NotMapped]
        public string ProfessionName { get; set; }

        public DateTime? DeadLine { get; set; }
        public DateTime? AssignDate { get; set; }
        public string AssignDetails { get; set; }
        public string AssignerName { get; set; }

        public DateTime? FinishDate { get; set; }
        public string FinishComment { get; set; }

        public DateTime? CheckDate { get; set; }
        public string CheckerUser { get; set; }
        public string CheckComment { get; set; }

    }
}
