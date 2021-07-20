using pizzeria.service.repositories;
using System;
using Xunit;

namespace pizzeria.service.tests
{
    public class PizzaTagRepositoryTests
    {
        [Fact]
        public void GetPizzaTagById()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new PizzaTagRepository(dbContext);

                var tag = sut.GetById(2);
                
                Assert.Equal(2, tag.Id);
                Assert.Equal("Gluténmentes", tag.Name);
            }
        }
    }
}
