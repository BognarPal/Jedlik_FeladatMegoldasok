using Microsoft.EntityFrameworkCore;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pizzeria.service
{
    public class ApplicationDbContext: DbContext
    {
        DbSet<Pizza> Pizzas { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<PizzaPizzaTag> PizzaPizzaTags { get; set; }
        DbSet<CustomerAddress> CustomerAddresses { get; set; }
        DbSet<UserRole> userRoles { get; set; }

        //TODO: connection string "kivezetése" config állományba
#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost; Database=pizzeria; Uid=root; Pwd=");
        }
#endif

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Egyediségek biztosítása
            modelBuilder.Entity<PizzaTag>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();

            //1:N kapcsolatok definiálása -> Interface-ek használata miatt van rá szükség
            modelBuilder.Entity<Order>()
                        .HasOne<Customer>(o => (Customer)o.Customer)
                        .WithMany()
                        .IsRequired();

            modelBuilder.Entity<Order>()
                        .HasOne<Address>(o => (Address)o.Address)
                        .WithMany()
                        .IsRequired(); 

            modelBuilder.Entity<PizzaPrice>()
                        .HasOne<Pizza>(p => (Pizza)p.Pizza)
                        .WithMany()
                        .IsRequired();

            //Kezdő adatok
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "admin" },
                new Role() { Id = 2, Name = "customer" },
                new Role() { Id = 3, Name = "kitchen" },
                new Role() { Id = 4, Name = "courier" }
            );
        }
    }
}
