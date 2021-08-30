using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pizzeria.data.interfaces.operations;
using pizzeria.service.models;
using System.Security.Authentication;

namespace pizzeria.website.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly string jwtSecret;
        private readonly int jwtValidityMinute;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            var jwtConfigSection = configuration.GetSection("Jwt");
            jwtSecret = jwtConfigSection["secret"];
            try
            {
                jwtValidityMinute = int.Parse(jwtConfigSection["validityMinute"]);
            }
            catch
            {
                jwtValidityMinute = 30;
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Authenticate model)
        {
            try
            {
                var user = (User)authRepository.AuthenticateUser(model, jwtSecret, jwtValidityMinute);
                return Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Token,
                    user.Phone,
                    Roles = user.Roles.Select(r => r.Name)
                });
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
#if DEBUG
                return BadRequest(new { message = ex.Message });
#else
                return BadRequest();
#endif
            }
        }

        //[Authorize(Roles = "admin")]
        [HttpGet("helloworld")]
        public IActionResult HelloWorld()
        {
            return Ok(new { Message = "Hello World" });
        }
    }
}
