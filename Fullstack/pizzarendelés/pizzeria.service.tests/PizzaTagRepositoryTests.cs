using pizzeria.service.repositories;
using System;
using Xunit;

namespace pizzeria.service.tests
{
    public class PizzaTagRepositoryTests
    {
        [Fact]
        public void Id_2_Should_Glutenmentes()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                //Arrange
                var sut = new PizzaTagRepository(dbContext);
                
                //Act
                var tag = sut.GetById(2);

                //Assert
                Assert.Equal(2, tag.Id);
                Assert.Equal("Gluténmentes", tag.Name);
            }

        }
    }
}
