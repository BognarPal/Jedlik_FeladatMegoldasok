using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.repositories
{
    public class AuthRepository : IAuthRepository<User>
    {
        private readonly ApplicationDbContext dbContext;

        public AuthRepository(ApplicationDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        public User AuthenticateUser(User user)
        {
            var foundUser = dbContext.Set<User>().FirstOrDefault(u => u.Email == user.Email);
            if (foundUser == null)
                throw new AuthenticationException("Nem létező e-mail cím, vagy hibás jelszó");
            
            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, foundUser.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
                throw new AuthenticationException("Nem létező e-mail cím, vagy hibás jelszó");

            return foundUser;
        }
    }
}
