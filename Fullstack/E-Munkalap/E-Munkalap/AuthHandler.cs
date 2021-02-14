using E_Munkalap.DTO.Authentication;
using E_Munkalap.SQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace E_Munkalap
{
    public class AuthHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private int sessionExpireTimeInMinute = 120;
        private readonly DatabaseProvider databaseProvider;

        public AuthHandler(
           IOptionsMonitor<AuthenticationSchemeOptions> options,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock,
           IOptions<DatabaseProvider> dbProvider,
           IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            this.sessionExpireTimeInMinute = configuration.GetValue<int>("AppSettings:SessionExpireTimeInMinute");
            this.databaseProvider = dbProvider.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers["Authorization"];
            if (!authorizationHeader.Any())
                return AuthenticateResult.Fail("Missing authorization header");

            var sessionid = authorizationHeader.ToString();
            if (string.IsNullOrWhiteSpace(sessionid))
                return AuthenticateResult.Fail("Invalid authorization header");

            try
            {
                var result = databaseProvider.Query<SessionModel, string>("authentication.userSelectBySessionId", new { sessionid });
                var session = result.Item1[0];
                var roles = result.Item2;
                if (session != null && session.LastAccess.AddMinutes(sessionExpireTimeInMinute) >= DateTime.Now)
                {
                    databaseProvider.Execute("authentication.sessionUpdateLastAccess", new { sessionid });
                    var claims = new List<Claim>()
                    {
                        new Claim("AdLoginName", session.AdLoginName),
                    };

                    roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

                    // create a new claims identity and return an AuthenticationTicket with the correct scheme
                    var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);

                    var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties(), Scheme.Name);

                    Context.Response.Headers.Add("session", JsonConvert.SerializeObject(new
                    {
                        session.AdLoginName,
                        name = session.UserName,
                        token = sessionid,
                        lastAccess = session.LastAccess,
                        validTo = session.LastAccess.AddMinutes(sessionExpireTimeInMinute),
                        roles
                    }));
                    Context.Response.Headers.Add("access-control-expose-headers", "session");

                    return await Task.FromResult(AuthenticateResult.Success(ticket));
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Sikertelen authentikáció");
            }
            return AuthenticateResult.Fail("Sikertelen authentikáció");
        }
    }
}
