using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace pizzeria.service.tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void GetById()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);

                var customer1 = sut.GetById(4);
                var customer2 = sut.GetById(2);

                Assert.Equal(4, customer1.Id);
                Assert.Null(customer2);
                Assert.Equal("Rend Elek", customer1.Name);
                Assert.Equal("elek@email.com", customer1.Email);
                Assert.Single(customer1.Roles);
                Assert.Equal("customer", customer1.Roles.First().Name);
                Assert.Equal("Futrinka u. 10", customer1.PrimaryAddress.StreetAndHouseNumber);
                Assert.Equal(2, customer1.Addresses.Count());
                Assert.Contains(customer1.Addresses, a => a.StreetAndHouseNumber == "Futrinka u. 10");
                Assert.Contains(customer1.Addresses, a => a.StreetAndHouseNumber == "Szent István út 7");
            }
        }

        [Fact]
        public void GetAllCustomers()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);

                var allCustomer = sut.GetAll();

                Assert.Single(allCustomer);
                Assert.Equal(4, allCustomer.First().Id);
                Assert.Equal("Rend Elek", allCustomer.First().Name);
                Assert.Equal("elek@email.com", allCustomer.First().Email);
                Assert.Single(allCustomer.First().Roles);
                Assert.Equal("customer", allCustomer.First().Roles.First().Name);
                Assert.Equal("Futrinka u. 10", allCustomer.First().PrimaryAddress.StreetAndHouseNumber);
                Assert.Equal(2, allCustomer.First().Addresses.Count());
                Assert.Contains(allCustomer.First().Addresses, a => a.StreetAndHouseNumber == "Futrinka u. 10");
                Assert.Contains(allCustomer.First().Addresses, a => a.StreetAndHouseNumber == "Szent István út 7");

            }
        }

        [Fact]
        public void AddNewCustomer()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);
                var newCustomer = new Customer()
                {
                    Name = "Winch Eszter",
                    Email = "eszter@email.com",
                    Phone = "06 55 111222",
                    PasswordHash = "pwdhash",        // TODO: Hash-t jelszóból előállítani !                   
                    PrimaryAddress = new Address() { City = "Győr", StreetAndHouseNumber = "Kiss körtér 1.", FloorAndDoor = "Fsz. 1", Comment = "" },
                    Addresses = new Address[] { new Address() { City = "Győr", StreetAndHouseNumber = "Fűz fasor 2.", FloorAndDoor = "", Comment = "" } }
                };                
                sut.Add(newCustomer);
                sut.Save();

                var savedCustomer = sut.GetById(newCustomer.Id);

                Assert.Equal(2, sut.GetAll().Count());
                Assert.Equal("Winch Eszter", savedCustomer.Name);
                Assert.Single(savedCustomer.Roles);
                Assert.Equal("customer", savedCustomer.Roles.First().Name);
                Assert.Equal("Kiss körtér 1.", savedCustomer.PrimaryAddress.StreetAndHouseNumber);
                Assert.Equal(2, savedCustomer.Addresses.Count());
                Assert.Contains(savedCustomer.Addresses, a => a.StreetAndHouseNumber == "Kiss körtér 1.");
                Assert.Contains(savedCustomer.Addresses, a => a.StreetAndHouseNumber == "Fűz fasor 2.");
            }
        }

        [Fact]
        public void AddMoreCustomer()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.AddRange(new List<Customer>() { }));
            }
        }


        [Fact]
        //TODO érdemes lenne meggondolni a deleted mező bevezetését (GDPR ??)
        public void RemoveCustomerWithId4()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);
                var Customer = sut.GetById(4);
                sut.Remove(Customer);
                sut.Save();

                Assert.Empty(sut.GetAll());
                Assert.Null(sut.GetById(4));
            }
        }

        [Fact]
        public void RemoveMoreCustomers()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new CustomerRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.RemoveRange(new List<Customer>() { }));
            }
        }

    }
}
