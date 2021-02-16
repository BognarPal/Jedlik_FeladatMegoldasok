using E_Munkalap.DTO.Authentication;
using E_Munkalap.DTO.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Munkalap.DTO
{
    public class MunkalapContext: DbContext
    {
        public DbSet<SessionModel> Sessions { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeProfession> EmployeeProfessions { get; set; }
        public DbSet<Work.Work> Works { get; set; }

        public MunkalapContext(DbContextOptions<MunkalapContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            #region UniqeAttribute feldolgozása
            {
                foreach (var property in this.GetType().GetProperties())
                    if (property.PropertyType.Name.StartsWith("DbSet") && property.PropertyType.IsGenericType)
                        try
                        {
                            foreach (var p in property.PropertyType.GenericTypeArguments[0].GetProperties())
                                if (p.GetCustomAttributes(typeof(UniqueAttribute), true).Length != 0)
                                {
                                    modelBuilder.Entity(property.PropertyType.GenericTypeArguments[0]).HasIndex(new string[] { p.Name }).IsUnique();
                                }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Hiba a uniqe beállítása során: {0}", ex.Message);
                        }
            }
            #endregion UniqeAttribute feldolgozása

            modelBuilder.Entity<Profession>().HasData(
                new Profession() { Id = 1, Name = "Lakatos" },
                new Profession() { Id = 2, Name = "Asztalos" },
                new Profession() { Id = 3, Name = "Takarító" },
                new Profession() { Id = 4, Name = "Segédmunkás" },
                new Profession() { Id = 5, Name = "Kőműves" },
                new Profession() { Id = 6, Name = "Burkoló" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Kiss Károly", AdLoginName = "kiss.karoly@dolgozo.jedlik.eu" },
                new Employee() { Id = 2, Name = "Ügyes Béla" },
                new Employee() { Id = 3, Name = "Mindenes Árpád" }
            );

            modelBuilder.Entity<EmployeeProfession>().HasData(
                new EmployeeProfession() { Id = 1, ProfessionId = 1, EmployeeId = 1 },
                new EmployeeProfession() { Id = 2, ProfessionId = 3, EmployeeId = 1 },
                new EmployeeProfession() { Id = 3, ProfessionId = 4, EmployeeId = 1 },
                new EmployeeProfession() { Id = 4, ProfessionId = 1, EmployeeId = 2 },
                new EmployeeProfession() { Id = 5, ProfessionId = 2, EmployeeId = 2 },
                new EmployeeProfession() { Id = 6, ProfessionId = 3, EmployeeId = 2 },
                new EmployeeProfession() { Id = 7, ProfessionId = 4, EmployeeId = 2 },
                new EmployeeProfession() { Id = 8, ProfessionId = 5, EmployeeId = 2 },
                new EmployeeProfession() { Id = 9, ProfessionId = 6, EmployeeId = 2 },
                new EmployeeProfession() { Id = 10, ProfessionId = 3, EmployeeId = 3 },
                new EmployeeProfession() { Id = 11, ProfessionId = 4, EmployeeId = 3 }
            );
            
        }
    }
}
