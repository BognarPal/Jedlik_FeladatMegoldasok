
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618
[Table("car")]
public class CarModel
{
    public int Id { get; set; }
    [Required]
    public string LicensePlateNumber { get; set; }
    [Required]
    public string FactoryAndModel { get; set; }
}
#pragma warning restore CS8618