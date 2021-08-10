using pizzeria.data.interfaces.models;
using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace pizzeria.service.tests
{
    public class PizzaRepositoryTests
    {
        [Fact]
        public void GetPizzaById()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                //Arrange
                var sut = new PizzaRepository(dbContext);

                //Act
                var pizza1 = sut.GetById(1);
                var pizza2 = sut.GetById(2);
                var pizza3 = sut.GetById(3);
                var pizza4 = sut.GetById(4);
                var pizza5 = sut.GetById(5);

                //Assert
                Assert.Equal(1, pizza1.Id);
                Assert.Equal(2, pizza2.Id);
                Assert.Equal(3, pizza3.Id);
                Assert.Equal(4, pizza4.Id);
                Assert.Equal("Margherita", pizza1.Name);
                Assert.Equal("Pepperoni", pizza2.Name);
                Assert.Equal("Gombás", pizza3.Name);
                Assert.Equal("Sonkás", pizza4.Name);
                Assert.Single(pizza2.Pictures);
                Assert.Equal(10, pizza2.Pictures.First().Picture.Length);
                Assert.Equal(10, pizza2.Pictures[0].Picture[9]);
                Assert.Null(pizza5);
                Assert.Single(pizza2.Tags);
                Assert.Equal(2, pizza3.Tags.Count());
                Assert.Equal(1190, pizza1.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1290, pizza2.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1240, pizza3.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1350, pizza4.Prices.First(p => p.ToDate == null).Price);
            }
        }

        [Fact]
        public void GetAllPizzas()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);
                var pizzas = sut.GetAll().OrderBy(p => p.Id).ToList();

                Assert.Equal(4, pizzas.Count);
                Assert.Equal(1, pizzas[0].Id);
                Assert.Equal("Pepperoni", pizzas[1].Name);
                Assert.Empty(pizzas[0].Tags);
                Assert.Single(pizzas[1].Tags);
                Assert.Equal("Csípős", pizzas[1].Tags[0].Name);
            }
        }

        [Fact]
        public void AddNewPizza()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);
                var pizzaTagRepository = new PizzaTagRepository(dbContext);
                var newPizza = new Pizza()
                {
                    Name = "Salami sensitive",
                    Description = "Paradicsom, szalámi, hagyma, laktózmentes sajt",
                    Tags = new List<PizzaTag>()
                    {
                        pizzaTagRepository.GetById(3)
                    },
                    Pictures = new List<PizzaPicture>()
                    {
                        new PizzaPicture() { Picture = new byte[] { 0, 0, 0, 0, 0 } },
                        new PizzaPicture() { Picture = new byte[] { 1, 1, 1, 1, 1 } }
                    },
                    Prices = new List<PizzaPrice>()
                    {
                        new PizzaPrice() { FromDate= new DateTime(2020, 01, 01), ToDate = new DateTime(2020, 12, 31), Price = 1140m },
                        new PizzaPrice() { FromDate= new DateTime(2021, 01, 01), ToDate = null, Price = 1240m }
                    }
                };

                var savedPizza = sut.Add(newPizza);
                sut.Save();

                Assert.Equal(5, savedPizza.Id);
                Assert.Equal("Salami sensitive", sut.GetById(5).Name);
                Assert.Single(sut.GetById(5).Tags);
                Assert.Equal(3, sut.GetById(5).Tags[0].Id);
                Assert.Equal(2, sut.GetById(5).Pictures.Count());
                Assert.Equal(0, sut.GetById(5).Pictures[0].Picture[0]);
                Assert.Equal(1, sut.GetById(5).Pictures[1].Picture[0]);
                Assert.Equal(1240, sut.GetById(5).Prices.FirstOrDefault(p => p.ToDate == null).Price);
            }
        }

        [Fact]
        public void AddMorePizzas()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.AddRange(new List<Pizza>() { }));
            }
        }

        [Fact]
        public void RemovePizzaWithId2()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);
                var pizza = sut.GetById(2);

                sut.Remove(pizza);
                sut.Save();

                Assert.Equal(3, sut.GetAll().Count());
                Assert.Null(sut.GetById(2));
                //TODO: biztos törölhető a pizza, ha már volt hozzá korábban megrendelés ??? Lehet, hogy inkább "deleted" mező kellene?
            }
        }

        [Fact]
        public void RemoveMorePizzas()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);

                Assert.Throws<NotSupportedException>(() => sut.RemoveRange(new List<Pizza>() { }));
            }
        }

        [Fact]
        public void UpdatePizza()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);
                var pizzaTagRepository = new PizzaTagRepository(dbContext);
                var pizza = sut.GetById(2);

                pizza.Name = "Salami, pepperoni";
                pizza.Description = "Paradicsom, sajt, szalámi, csípős pepperóni";
                pizza.Tags.Clear();
                pizza.Tags.Add(pizzaTagRepository.GetById(2));
                pizza.Tags.Add(pizzaTagRepository.GetById(3));
                pizza.Pictures.Clear();                
                pizza.Pictures.Add(new PizzaPicture() { Picture = new byte[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 } });
                pizza.Pictures.Add(new PizzaPicture() { Picture = new byte[] { 18, 17, 16, 15, 14, 13, 12, 11 } });
                
                pizza.Prices.FirstOrDefault(p => p.ToDate == null).ToDate = DateTime.Today.AddDays(-1);
                pizza.Prices.Add(new PizzaPrice()
                {
                    FromDate = DateTime.Today,
                    ToDate = null,
                    Price = 1399
                });

                var updatedPizza = sut.Update(pizza);
                sut.Save();

                var pizzaWithId2 = sut.GetById(2);
                Assert.Equal(2, updatedPizza.Id);
                Assert.Equal("Salami, pepperoni", pizzaWithId2.Name);
                Assert.Equal("Paradicsom, sajt, szalámi, csípős pepperóni", pizzaWithId2.Description);
                Assert.Equal(2, pizzaWithId2.Pictures.Count);
                Assert.Equal(9, pizzaWithId2.Pictures[0].Picture[0]);
                Assert.Equal(18, pizzaWithId2.Pictures[1].Picture[0]);
                Assert.Equal(2, pizzaWithId2.Tags.Count);
                Assert.Equal(2, pizzaWithId2.Tags[0].Id);
                Assert.Equal(3, pizzaWithId2.Tags[1].Id);
                Assert.Equal(3, pizzaWithId2.Prices.Count);
                Assert.Equal(1399, pizzaWithId2.Prices.FirstOrDefault(p => p.ToDate == null).Price);
            }
        }

        [Fact]
        public void SearchPizzaWhereDescriptionContainsSzalámi()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);

                var pizzas = sut.Search(p => p.Description.Contains("szalámi")).ToList();

                Assert.Single(pizzas);
                Assert.Equal(2, pizzas[0].Id);
                Assert.Equal("Pepperoni", pizzas[0].Name);
                Assert.Single(pizzas[0].Tags);
                Assert.Equal("Csípős", pizzas[0].Tags[0].Name);
                Assert.Single(pizzas[0].Pictures);
                Assert.Equal(10, pizzas[0].Pictures[0].Picture.Length);
                Assert.Equal(10, pizzas[0].Pictures[0].Picture[9]);
            }
        }

        [Fact]
        public void SearchPizzaWhereDescriptionContainsParadicsom()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaRepository(dbContext);

                var pizzas = sut.Search(p => p.Description.Contains("Paradicsom"));

                Assert.Equal(4, pizzas.Count());
                var pizza1 = pizzas.FirstOrDefault(p => p.Id == 1);
                var pizza2 = pizzas.FirstOrDefault(p => p.Id == 2);
                Assert.NotNull(pizza1);
                Assert.NotNull(pizza2);
                Assert.Equal("Margherita", pizza1.Name);
                Assert.Equal("Pepperoni", pizza2.Name);
                Assert.Empty(pizza1.Tags);
                Assert.Single(pizza2.Tags);
                Assert.Equal("Csípős", pizza2.Tags[0].Name);
                Assert.Single(pizza2.Pictures);
                Assert.Equal(10, pizza2.Pictures[0].Picture.Length);
                Assert.Equal(10, pizza2.Pictures[0].Picture[9]);
            }
        }
    }
}
