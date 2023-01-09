using Microsoft.EntityFrameworkCore;

public class AppContext: DbContext{

#pragma warning disable CS8618
    public DbSet<RouteModel> Routes {get; set;}

    public AppContext(DbContextOptions options): base(options)
    {
    }

#pragma warning restore CS8618

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseInMemoryDatabase($"TravelAgency");  
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>().HasIndex(k => k.LicensePlateNumber).IsUnique(); 

        modelBuilder.Entity<CarModel>().HasData(                
            new CarModel() {Id = 1, LicensePlateNumber = "ABC123", FactoryAndModel = "Barkas"},
            new CarModel() {Id = 2, LicensePlateNumber = "AACD123", FactoryAndModel = "Tesla Model 3"},
            new CarModel() {Id = 3, LicensePlateNumber = "GDE563", FactoryAndModel = "Volkswagen Transporter"},
            new CarModel() {Id = 4, LicensePlateNumber = "REl379", FactoryAndModel = "Suzuki Vitara"},
            new CarModel() {Id = 5, LicensePlateNumber = "ACEP345", FactoryAndModel = "Ford Transit"}
        );  

        modelBuilder.Entity<RouteModel>().HasData(
            new RouteModel() 
            { 
                Id = 1, 
                CarId = 3,
                Date = DateTime.Now.AddDays(-10),
                DriverName = "Béla bácsi",
                To = "Budapest",
                From = "Győr",
                Km = 87123
            },
            new RouteModel()
            {
                Id = 2,
                CarId = 3,
                Date = DateTime.Now.AddDays(-10),
                DriverName = "Béla bácsi",
                To = "Székesfehérvár",
                From = "Budapest",
                Km = 87185
            },
            new RouteModel()
            {
                Id = 3,
                CarId = 3,
                Date = DateTime.Now.AddDays(-10),
                DriverName = "Béla bácsi",
                To = "Székesfehérvár",
                From = "Budapest",
                Km = 87256
            }
        );                                                          

    }
}