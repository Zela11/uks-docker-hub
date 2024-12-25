using DockerHubBackend.DTO;
using DockerHubBackend.IRepository;
using DockerHubBackend.IService;
using DockerHubBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DockerHubBackend.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        public IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService userService)
        {
            _authService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenDTO>> Login([FromBody] CredentialsDTO credentialsDto)
        {

            if (credentialsDto == null) return BadRequest("Invalid credentials");
            var token = await _authService.Login(credentialsDto.Email, credentialsDto.Password);
            if (token == null)
            {
                return BadRequest("Invalid credentials");
            }
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register()
        {
            UserDTO user = new UserDTO
            {
                Password = "sifraaaa",
                Email = "email",
                Name = "ime",
                Surname = "prezime",
                Birthday = "rodj"
            };

            bool isCreated = await _authService.Register(user);

            if (isCreated)
            {
                return Ok(isCreated);
            } else
            {
                return BadRequest(isCreated);
            }
        }
    }
}
