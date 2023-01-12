using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngatlanWebAPI.EF
{
    [Table("ingatlanok")]
    public class Ingatlan
    {
        public int Id { get; set; }
        public int KategoriaId { get; set; }
        public Kategoria Kategoria { get; set; }
        [MaxLength(500)]
        public string Leiras { get; set; }
        public DateTime HirdetesDatuma { get; set; }
        public bool Tehermentes { get; set; }
        [MaxLength(200)]
        public string kepUrl { get; set; }
    }
}
