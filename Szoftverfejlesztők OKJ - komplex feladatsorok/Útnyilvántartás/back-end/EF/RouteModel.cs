using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618
[Table("routes")]
public class RouteModel 
{
    public int Id { get; set; }
    [Required]
    public CarModel Car { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string From { get; set; }
    [Required]
    public string To { get; set; }
    public int Km { get; set; }
    [Required]
    public string DriverName { get; set; }

    public int CarId { get; set; }

}
#pragma warning restore CS8618
