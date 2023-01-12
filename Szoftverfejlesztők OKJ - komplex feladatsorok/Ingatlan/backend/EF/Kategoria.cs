using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngatlanWebAPI.EF
{
    [Table("kategoriak")]
    public class Kategoria
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Megnevezes { get; set; }
    }
}
