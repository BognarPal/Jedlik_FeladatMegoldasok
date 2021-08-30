using pizzeria.service.models;
using pizzeria.service.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Xunit;

namespace pizzeria.service.tests
{
    public class AuthRepositoryTests
    {
        [Fact]
        public void SuccessAuthenticate()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new AuthRepository(dbContext);

                var user = new User() { Email = "admin@localhost.com", Password = "admin" };
                var foundUser = sut.AuthenticateUser(user);

                Assert.Equal(1, foundUser.Id);
            }
        }

        [Fact]
        public void UnSuccessAuthenticate()
        {
            using (var dbContext = TestDbContext.CreateDbContext())
            {
                var sut = new AuthRepository(dbContext);

                var user = new User() { Email = "admin@localhost.com", Password = "valami" };
                var user2 = new User() { Email = "senki@localhost.com", Password = "valami" };

                Assert.Throws<AuthenticationException>(() => sut.AuthenticateUser(user));
                Assert.Throws<AuthenticationException>(() => sut.AuthenticateUser(user2));
            }
        }
    }
}
