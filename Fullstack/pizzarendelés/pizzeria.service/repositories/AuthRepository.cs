using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pizzeria.data.interfaces.models;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AuthRepository(ApplicationDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        public IUser AuthenticateUser(IAuthenticate user)
        {
            var foundUser = dbContext.Set<User>()
                                     .Include(u => u.UserRoles).ThenInclude(r => r.Role)
                                     .FirstOrDefault(u => u.Email == user.Email);
            if (foundUser == null)
                throw new AuthenticationException("Nem létező e-mail cím, vagy hibás jelszó");

            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(foundUser, foundUser.PasswordHash, user.Password) == PasswordVerificationResult.Failed)
                throw new AuthenticationException("Nem létező e-mail cím, vagy hibás jelszó");

            return foundUser;
        }

        public IUser AuthenticateUser(IAuthenticate user, string jwtSecretString, int jwtValidityMinute)
        {
            var foundUser = (User)AuthenticateUser(user);

            // authentication successful so generate jwt token
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, foundUser.Email));
            foundUser.Roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.Name)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecretString);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(jwtValidityMinute),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            foundUser.Token = tokenHandler.WriteToken(token);

            return foundUser;
        }
    }
}
