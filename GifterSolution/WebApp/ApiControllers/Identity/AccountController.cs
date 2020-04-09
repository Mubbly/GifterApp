using System;
using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            var appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {loginDTO.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api login. User {loginDTO.Email} attempted login with bad password!");
                return StatusCode(403);
            }

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); // get the User analog
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays"));
            
            _logger.LogInformation($"Web-Api login. Token generated for user {loginDTO.Email}");
            return Ok(new {token = jwt, status = "Logged in"});
        }

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName
            };
            
            // TODO: Why is email format not validated by Identity?
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api login. User {registerDTO.Email} could not be created!");
                return StatusCode(403);
            }
            
            _logger.LogInformation($"Web-Api login. User {registerDTO.Email} registered!");
            return Ok();
        }

        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
            
        }

        public class RegisterDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            
        }
    }
}