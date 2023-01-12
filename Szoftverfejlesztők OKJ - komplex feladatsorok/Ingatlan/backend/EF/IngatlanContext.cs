using Microsoft.EntityFrameworkCore;
using System;

namespace IngatlanWebAPI.EF
{
    public class IngatlanContext: DbContext
    {
        public DbSet<Ingatlan> Ingatlanok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase($"ingatlan");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategoria>().HasIndex(k => k.Megnevezes).IsUnique();

            modelBuilder.Entity<Kategoria>().HasData(                
                new Kategoria() {Id = 1, Megnevezes = "Ház"},
                new Kategoria() {Id = 2, Megnevezes = "Lakás"},
                new Kategoria() {Id = 3, Megnevezes = "Építési telek"},
                new Kategoria() {Id = 4, Megnevezes = "Garázs"},
                new Kategoria() {Id = 5, Megnevezes = "Mezőgazdasági terület"},
                new Kategoria() {Id = 6, Megnevezes = "Ipari ingatlan" }
            );

            modelBuilder.Entity<Ingatlan>().HasData(
                new Ingatlan() { Id = 1, KategoriaId = 1, Leiras = "Kertvárosi részén, egyszintes házat kínálunk nyugodt környezetben, nagy telken.", HirdetesDatuma = DateTime.Today.AddDays(-10), Tehermentes = true, kepUrl = "https://cdn.jhmrad.com/wp-content/uploads/three-single-storey-houses-elegance-amazing_704000.jpg" },
                new Ingatlan() { Id = 2, KategoriaId = 1, Leiras = "Belvárosi környezetben, árnyékos helyen kis méretú családi ház eladó. Tömegközlekedéssel könnyen megközelíthető.", HirdetesDatuma = DateTime.Today.AddDays(-3), Tehermentes = true, kepUrl = "https://www.westsideseattle.com/sites/default/files/styles/news_teaser/public/images/archive/ballardnewstribune.com/content/articles/2008/11/21/features/columnists/column07.jpg?itok=wMrlOwFU" },
                new Ingatlan() { Id = 3, KategoriaId = 2, Leiras = "Kétszintes berendezett lakás a belvárosban kiadó.", HirdetesDatuma = DateTime.Today.AddDays(-7), Tehermentes = true, kepUrl = "https://images.unsplash.com/photo-1606074280798-2dabb75ce10c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80" },
                new Ingatlan() { Id = 4, KategoriaId = 4, Leiras = "Nagy garázs kertvárosi környezetben kiadó", HirdetesDatuma = DateTime.Today.AddDays(-5), Tehermentes = true, kepUrl = "https://www.afritechmedia.com/wp-content/uploads/2021/11/How-Can-You-Protect-Your-Garage-Floor-612x340.jpg" },
                new Ingatlan() { Id = 5, KategoriaId = 5, Leiras = "10 hektáros mezőhazdasági terület eladó", HirdetesDatuma = DateTime.Today.AddDays(-1), Tehermentes = true, kepUrl = "https://i2-prod.manchestereveningnews.co.uk/incoming/article18411144.ece/ALTERNATES/s810/0_gettyimages-1151774950-170667a.jpg" },
                new Ingatlan() { Id = 6, KategoriaId = 6, Leiras = "Felújításra szoruló üzemcsarnok zöld környezetben áron alul eladó", HirdetesDatuma = DateTime.Today.AddDays(-8), Tehermentes = false, kepUrl = "https://cdn.pixabay.com/photo/2019/01/31/09/24/urban-3966306_960_720.jpg" }
            );
        }
    }
}
