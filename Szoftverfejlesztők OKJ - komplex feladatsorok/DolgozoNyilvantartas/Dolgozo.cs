using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DolgozoNyilvantartas
{
    [Table("Dolgozok")]
    public class Dolgozo
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public string AdoazonositoJel { get; set; }

        [StringLength(150)]
        [Required]
        public string Nev { get; set; }

        public int EvesSzabadsag { get; set; }

        [Required]
        public SzervezetiEgyseg SzervezetiEgyseg { get; set; }

        [StringLength(100)]
        public string Beosztas { get; set; }

        public DateTime? FelvetelDatuma { get; set; }

    }
}
