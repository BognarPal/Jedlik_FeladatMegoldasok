using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DolgozoNyilvantartas
{
    [Table("SzervezetiEgysegek")]
    public class SzervezetiEgyseg
    {
        /*
        Create Table SzervezetiEgyseg (
            Id int not null auto_increment primary key,
            Nev varchar(100) not null unique
        )
        */

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nev { get; set; }
    }
}
