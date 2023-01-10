using Microsoft.EntityFrameworkCore;

namespace back_end.EF
{
    public class AppContext: DbContext
    {
        public DbSet<OrderModel> Orders { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaModel>().HasData(
                new PizzaModel() { Id = 1, Name = "Margarita", Price = 2190 },
                new PizzaModel() { Id = 2, Name = "Sonkás", Price = 2290 },
                new PizzaModel() { Id = 3, Name = "Szalámis", Price = 2690 },
                new PizzaModel() { Id = 4, Name = "Hawaii", Price = 2690 },
                new PizzaModel() { Id = 5, Name = "Tonno", Price = 2790 }
            );

            modelBuilder.Entity<OrderModel>().HasData(
                new { Id = 1, PizzaId = 1, Name = "Teszt Elek", Address = "Nagy tér 1.", Quantity = 2, Comment = "" },
                new { Id = 2, PizzaId = 3, Name = "Teszt Elek", Address = "Nagy tér 1.", Quantity = 1, Comment = "" },
                new { Id = 3, PizzaId = 4, Name = "Teszt Elek", Address = "Nagy tér 1.", Quantity = 1, Comment = "Ananász nélkül"}
            );
        }
    }
}
