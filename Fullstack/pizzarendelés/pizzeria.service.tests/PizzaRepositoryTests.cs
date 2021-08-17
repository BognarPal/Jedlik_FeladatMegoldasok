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
                Assert.Equal("Csípős", pizza2.Tags[0].Name);
                Assert.Equal(1190, pizza1.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1290, pizza2.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1240, pizza3.Prices.First(p => p.ToDate == null).Price);
                Assert.Equal(1350, pizza4.Prices.First(p => p.ToDate == null).Price);
            }
        }
    }
}
