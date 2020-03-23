using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DolgozoNyilvantartas
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<SzervezetiEgyseg> SzervezetiEgysegek { get; set; }
        public DbSet<Dolgozo> Dolgozok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost; Database=dolgozok; Uid=root; Pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SzervezetiEgyseg>().HasIndex(sz => sz.Nev).IsUnique();
            modelBuilder.Entity<Dolgozo>().HasIndex(d => d.AdoazonositoJel).IsUnique();

            modelBuilder.Entity<SzervezetiEgyseg>().HasData(
                new SzervezetiEgyseg() { Id = 1, Nev = "Igazgatóság"},
                new SzervezetiEgyseg() { Id = 2, Nev = "Termelés"},
                new SzervezetiEgyseg() { Id = 3, Nev = "Marketing"}
            );
        }
    }
}
