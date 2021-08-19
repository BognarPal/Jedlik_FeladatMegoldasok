using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void GetAllPizzaTags()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                
                var allTags = sut.GetAll();

                Assert.Equal(4, allTags.Count());
                Assert.Equal(1, allTags.OrderBy(t => t.Id).First().Id);
                Assert.Equal("Gluténmentes", allTags.OrderBy(t => t.Id).Skip(1).First().Name);
            }
        }

        [Fact]
        public void AddNewPizzaTag()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                var tag = new PizzaTag()
                {
                    Name = "Cukormentes"
                };

                var currentTagsCount = sut.GetAll().Count();
                var savedTag = sut.Add(tag);
                sut.Save();

                Assert.Equal(currentTagsCount + 1, sut.GetAll().Count());
                Assert.Equal(5, savedTag.Id);
                Assert.Equal("Cukormentes", sut.GetById(5).Name);
            }
        }

        [Fact]
        public void AddThreeNewPizzaTags()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                List<PizzaTag> pizzaTags = new List<PizzaTag>()
                {
                    new PizzaTag() {Name = "Cukormentes"},
                    new PizzaTag() {Name = "Tojásmentes"},
                    new PizzaTag() {Name = "Tejmentes"}
                };
                var savedPizzaTags = sut.AddRange(pizzaTags);
                sut.Save();

                Assert.Equal(3, savedPizzaTags.Count());
                Assert.Equal(7, sut.GetAll().Count());
                Assert.NotEqual(0, savedPizzaTags.ToList()[0].Id);
                Assert.NotEqual(0, savedPizzaTags.ToList()[1].Id);
                Assert.NotEqual(0, savedPizzaTags.ToList()[2].Id);

                HashSet<string> names = new HashSet<string>()
                {
                    sut.GetById(savedPizzaTags.ToList()[0].Id).Name,
                    sut.GetById(savedPizzaTags.ToList()[1].Id).Name,
                    sut.GetById(savedPizzaTags.ToList()[2].Id).Name
                };
                Assert.Equal(3, names.Count);
                foreach (var name in names)
                {
                    Assert.Contains(name, new string[] { "Cukormentes", "Tojásmentes", "Tejmentes" });
                }
            }
        }

        [Fact]
        public void RemovePizzaTagWithId2()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                var tag = sut.GetById(2);
                sut.Remove(tag);
                sut.Save();

                Assert.Equal(3, sut.GetAll().Count());
                Assert.Null(sut.GetById(2));
            }
        }

        [Fact]
        public void RemoveTwoPizzaTags()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                var tags = new List<PizzaTag>()
                {
                    sut.GetById(1),
                    sut.GetById(3)
                };
                sut.RemoveRange(tags);
                sut.Save();

                Assert.Equal(2, sut.GetAll().Count()); 
                Assert.Null(sut.GetById(1));
                Assert.Null(sut.GetById(3));
            }
        }

        [Fact]
        public void UpdatePizzaTagWithId1()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                var tag = sut.GetById(1);
                tag.Name = "Cukormentes";
                var updatedTag = sut.Update(tag);
                sut.Save();

                Assert.Equal(tag.Id, updatedTag.Id);
                Assert.Equal("Cukormentes", updatedTag.Name);
                Assert.Equal("Cukormentes", sut.GetById(1).Name);
                Assert.Equal(4, sut.GetAll().Count());
            }
        }

        [Fact]
        public void SearchTagsWhereNameContains_Mentes()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                PizzaTagRepository sut = new PizzaTagRepository(dbContext);
                List<PizzaTag> mentesTags = sut.Search(t => t.Name.Contains("mentes")).ToList();

                Assert.Equal(2, mentesTags.Count);
                Assert.Contains("mentes", mentesTags[0].Name);
                Assert.Contains("mentes", mentesTags[1].Name);
                Assert.Contains(mentesTags[0].Id, new int[] { 2, 3 });
                Assert.Contains(mentesTags[1].Id, new int[] { 2, 3 });
                Assert.NotEqual(mentesTags[0].Id, mentesTags[1].Id);
            }
        }
    }
}
