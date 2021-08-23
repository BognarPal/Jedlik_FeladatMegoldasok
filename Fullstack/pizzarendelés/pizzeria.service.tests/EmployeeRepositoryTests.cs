using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace pizzeria.service.tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void GetById()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                //Arrange
                var sut = new EmployeeRepository(dbContext);
                
                //Act
                var employee1 = sut.GetById(1);
                var employee2 = sut.GetById(2);
                var employee4 = sut.GetById(4);

                //Assert
                Assert.Equal(1, employee1.Id);
                Assert.Equal(2, employee2.Id);
                Assert.Null(employee4);
                Assert.Equal("admin", employee1.Name);
                Assert.Equal("Szakács Béla", employee2.Name);
                Assert.Equal("admin@localhost.com", employee1.Email);
                Assert.Equal("bela@localhost.com", employee2.Email);
                Assert.Equal("admin", employee1.Roles.First().Name);
                Assert.Equal("kitchen", employee2.Roles.First().Name);
            }
        }

        [Fact]
        public void GetAllEmployees()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);
                
                var allEmployees = sut.GetAll();

                Assert.Equal(3, allEmployees.Count());
                Assert.Equal(1, allEmployees.OrderBy(t => t.Id).First().Id);
                Assert.Equal("Szakács Béla", allEmployees.OrderBy(t => t.Id).Skip(1).First().Name);
                Assert.Equal("admin", allEmployees.First(t => t.Id == 1).Roles.First().Name);
                Assert.Equal("kitchen", allEmployees.First(t => t.Id == 2).Roles.First().Name);
            }
        }

        [Fact]
        public void AddNewEmployee()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);
                var newEmployee = new Employee()
                {
                    Name = "Géza",
                    Email = "geza@localhost.com",
                    Phone = "06 90 111222",
                    PasswordHash = "pwdhash",        // TODO: Hash-t jelszóból elõállítani !                   
                };
                newEmployee.UserRoles = new List<UserRole>()
                {
                    new UserRole() { User = newEmployee, Role = new GenericRepository<Role>(dbContext).GetById(4) }
                };
                sut.Add(newEmployee);                

                sut.Save();

                var savedEmployee = sut.GetById(newEmployee.Id);

                Assert.Equal(4, sut.GetAll().Count());
                Assert.Equal("Géza", savedEmployee.Name);                
                Assert.Equal("courier", savedEmployee.Roles.First().Name);
            }
        }

        [Fact]
        public void AddMoreEmployees()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.AddRange(new List<Employee>() { }));
            }
        }

        [Fact]
        public void RemoveEmployeeWithId2()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);
                var employee = sut.GetById(2);
                sut.Remove(employee);
                sut.Save();

                Assert.Equal(2, sut.GetAll().Count());
                Assert.Null(sut.GetById(2));
            }
        }

        [Fact]
        public void RemoveMoreEmployees()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.RemoveRange(new List<Employee>() { }));
            }
        }

        [Fact]
        public void UpdateEmployeeWithId2()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);
                var employee = sut.GetById(2);
                employee.Name = "Béla bácsi";
                employee.PasswordHash = "Próba";
                sut.Update(employee);
                sut.Save();

                var updatedEmployee = sut.GetById(2);

                Assert.Equal(2, updatedEmployee.Id);
                Assert.Equal("Béla bácsi", updatedEmployee.Name);
                Assert.Equal("bela@localhost.com", updatedEmployee.Email);
                Assert.Equal("Próba", updatedEmployee.PasswordHash);
                Assert.Equal(3, sut.GetAll().Count());
            }
        }

        [Fact]
        public void SearchByName()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new EmployeeRepository(dbContext);
                List<Employee> admin = sut.Search(t => t.Name == "admin").ToList();

                Assert.Single(admin);
                Assert.Equal("admin@localhost.com", admin[0].Email);
                Assert.Equal("admin", admin[0].Roles.First().Name);

            }
        }
    }
}
