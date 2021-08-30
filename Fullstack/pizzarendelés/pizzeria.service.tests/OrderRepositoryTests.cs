using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace pizzeria.service.tests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void GetById()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                var order1 = sut.GetById(1);
                var order2 = sut.GetById(2);

                Assert.Equal(1, order1.Id);
                Assert.Null(order2);
                Assert.Equal("Szent István út 7", order1.Address.StreetAndHouseNumber);
                Assert.Equal("Rend Elek", order1.Customer.Name);
                Assert.Equal(2, order1.OrderDetails.Count());
                Assert.Contains(order1.OrderDetails, d => d.Pizza.Name == "Margherita");
                Assert.Contains(order1.OrderDetails, d => d.Pizza.Name == "Gombás");
            }
        }

        [Fact]
        public void GetAllOrders()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.GetAll());
            }
        }

        [Fact]
        public void AddNewOrder()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                var customer = new CustomerRepository(dbContext).GetById(4);
                var pizza = new PizzaRepository(dbContext).GetById(2);
                var order = new Order()
                {
                    Customer = customer,
                    Address = customer.PrimaryAddress,
                    OrderDetails = new List<OrderDetails>()
                    {
                        new OrderDetails() { Pizza = pizza, Count = 2}
                    },
                    CustomerComment = "megjegyzés"
                };

                sut.Add(order);
                sut.Save();

                var savedOrder = sut.GetById(order.Id);
                Assert.Equal(2, savedOrder.Id);
                Assert.Equal("Futrinka u. 10", savedOrder.Address.StreetAndHouseNumber);
                Assert.Equal("Rend Elek", savedOrder.Customer.Name);
                Assert.Single(savedOrder.OrderDetails);
                Assert.Equal("Pepperoni", savedOrder.OrderDetails.First().Pizza.Name);
                Assert.True(savedOrder.OrderTimeStamp.AddSeconds(5) > DateTime.Now);
                Assert.True(savedOrder.OrderTimeStamp <= DateTime.Now);
            }
        }

        [Fact]
        public void AddMoreOrders()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.AddRange(new List<Order>() { }));
            }
        }

        [Fact]
        public void RemoveOrder()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.Remove(new Order()));
            }
        }

        [Fact]
        public void RemoveMoreOrder()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.RemoveRange(new List<Order>() { } ));
            }
        }

        [Fact]
        public void UpdateOrder()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.Update(new Order()));
            }
        }

        [Fact]
        public void SearchOrder()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                var orders = sut.Search(o => o.Address.City == "Győr").ToList();

                Assert.Single(orders);
                Assert.Equal(1, orders[0].Id);
                Assert.Equal("Szent István út 7", orders[0].Address.StreetAndHouseNumber);
                Assert.Equal("Rend Elek", orders[0].Customer.Name);
                Assert.Equal(2, orders[0].OrderDetails.Count());
                Assert.Contains(orders[0].OrderDetails, d => d.Pizza.Name == "Margherita");
                Assert.Contains(orders[0].OrderDetails, d => d.Pizza.Name == "Gombás");
            }
        }

        [Fact]
        public void TimeStampCheck()
        {


            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new OrderRepository(dbContext);

                var order = sut.GetById(1);
                sut.StartCooking(order);
                sut.Save();

                var savedOrder = sut.GetById(1);

                Assert.NotNull(savedOrder.CookingStartTimeStamp);
                Assert.True(((DateTime)savedOrder.CookingStartTimeStamp).AddSeconds(5) > DateTime.Now);
                Assert.True(savedOrder.CookingStartTimeStamp <= DateTime.Now);
            }
        }

    }
}
