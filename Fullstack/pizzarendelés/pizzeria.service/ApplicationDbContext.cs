using Microsoft.EntityFrameworkCore;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //TODO a connection string-et ki kell emelni az application.json-ba
#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost; Database=pizzeria2; Uid=root; Pwd=;",
                new MariaDbServerVersion(new Version("10.4.20")));
                
        }
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Egyediségek biztosítása
            modelBuilder.Entity<PizzaTag>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();

            //Kezdő adatok
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "admin"},
                new Role() { Id = 2, Name = "customer"},
                new Role() { Id = 3, Name = "kitchen"},
                new Role() { Id = 4, Name = "courier"}
            );
        }
    }
}
