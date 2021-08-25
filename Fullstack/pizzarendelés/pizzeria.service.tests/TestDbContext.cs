using Microsoft.EntityFrameworkCore;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.tests
{
    public class TestDbContext: ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("pizzeria" + Guid.NewGuid().ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PizzaTag>().HasData(
                new PizzaTag() { Id = 1, Name = "Csípős" },
                new PizzaTag() { Id = 2, Name = "Gluténmentes" },
                new PizzaTag() { Id = 3, Name = "Laktózmentes" },
                new PizzaTag() { Id = 4, Name = "Vegetariánus" }
            );

            modelBuilder.Entity<Pizza>().HasData(
                new Pizza() { Id = 1, Name = "Margherita", Description = "Paradicsom, sajt, bazsalikom"},
                new Pizza() { Id = 2, Name = "Pepperoni", Description = "Paradicsom, sajt, szalámi, pepperóni", },                      
                new Pizza() { Id = 3, Name = "Gombás", Description = "Paradicsom, növényi sajt, erdei gombák"},
                new Pizza() { Id = 4, Name = "Sonkás", Description = "Paradicsom, sajt, szárított sonka" }
            );

            modelBuilder.Entity<PizzaPizzaTag>().HasData(
                new { Id = 1, PizzaId = 2, PizzaTagId = 1 },
                new { Id = 2, PizzaId = 3, PizzaTagId = 3 },
                new { Id = 3, PizzaId = 3, PizzaTagId = 4 }
            );

            modelBuilder.Entity<PizzaPrice>().HasData(
                new { Id = 1, PizzaId = 1, FromDate = new DateTime(2020, 01, 01), ToDate = new DateTime(2020, 12, 31), Price = 1090m },
                new { Id = 2, PizzaId = 1, FromDate = new DateTime(2021, 01, 01), Price = 1190m },
                new { Id = 3, PizzaId = 2, FromDate = new DateTime(2020, 01, 01), ToDate = new DateTime(2020, 12, 31), Price = 1190m },
                new { Id = 4, PizzaId = 2, FromDate = new DateTime(2021, 01, 01), Price = 1290m },
                new { Id = 5, PizzaId = 3, FromDate = new DateTime(2020, 01, 01), ToDate = new DateTime(2020, 12, 31), Price = 1140m },
                new { Id = 6, PizzaId = 3, FromDate = new DateTime(2021, 01, 01), Price = 1240m },
                new { Id = 7, PizzaId = 4, FromDate = new DateTime(2020, 01, 01), ToDate = new DateTime(2020, 12, 31), Price = 1250m },
                new { Id = 8, PizzaId = 4, FromDate = new DateTime(2021, 01, 01), Price = 1350m }
            );

            modelBuilder.Entity<PizzaPicture>().HasData(
               new { Id = 1, PizzaId = 2, Picture = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 } }
            );

            //TODO: teszt projektben admin felhasználó kezdő jelszavához tartozó hash létrehozása
            modelBuilder.Entity<Employee>().HasData(
                new User() { Id = 1, Name = "admin", Email = "admin@localhost.com", Phone = "+36 90 123456", PasswordHash = "TODO !!!" },
                new User() { Id = 2, Name = "Szakács Béla", Email = "bela@localhost.com", Phone = "06 90 654321", PasswordHash = "TODO !!!" },
                new User() { Id = 3, Name = "Futár Kálmán", Email = "kalman@localhost.com", Phone = "06 90 987654", PasswordHash = "TODO !!!" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new { Id = 1, UserId = 1, RoleId = 1 },
                new { Id = 2, UserId = 2, RoleId = 3 },
                new { Id = 3, UserId = 3, RoleId = 4 }
            );

            modelBuilder.Entity<Address>().HasData(
                new { Id = 1, City = "Győr", StreetAndHouseNumber = "Futrinka u. 10", FloorAndDoor = "2 em. 5", Comment = "Nyomd a kapucsengőt", CustomerId = 4 },
                new { Id = 2, City = "Győr", StreetAndHouseNumber = "Szent István út 7", FloorAndDoor = "", Comment = "Jedlik", CustomerId = 4 }
            );
            modelBuilder.Entity<Customer>().HasData(
                new { Id = 4, Name = "Rend Elek", Email = "elek@email.com", Phone = "06 55 1234567", PasswordHash = "TODO !!!", PrimaryAddressId = 1}
            );          
            modelBuilder.Entity<UserRole>().HasData(
                new { Id = 4, UserId = 4, RoleId = 2 }
            );

        }

        public static ApplicationDbContext CreateDbContext()
        {
            ApplicationDbContext dbContext = new TestDbContext();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
    }
}
