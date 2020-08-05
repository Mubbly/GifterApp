using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Identity;
using PublicApi.DTO.v1.Mappers;
using DomainIdentity=Domain.App.Identity; // Using Domain due to userManager and signInManager

namespace WebApp.ApiControllers._1._0.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<DomainIdentity.AppUser> _signInManager;
        private readonly UserManager<DomainIdentity.AppUser> _userManager;
        private readonly IAppBLL _bll;
        private readonly AppUserMapper _mapper = new AppUserMapper();
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="signInManager"></param>
        /// <param name="bll"></param>
        public AccountController(IConfiguration configuration, UserManager<DomainIdentity.AppUser> userManager,
            SignInManager<DomainIdentity.AppUser> signInManager, ILogger<AccountController> logger, IAppBLL bll)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _bll = bll;
        }

        /// <summary>
        ///     Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="loginDTO">Login data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // No such email found
            var appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {loginDTO.Email} not found!");
                return NotFound(new MessageDTO("User not found!"));
            }
            // No such password found
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDTO.Password, false);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api login. User {loginDTO.Email} not found!");
                return NotFound(new MessageDTO("User not found!"));
            }
            
            await _userManager.UpdateAsync(appUser);
            await _bll.SaveChangesAsync();

            // Log user in
            return await LogIn(appUser);
        }

        /// <summary>
        ///     Endpoint for user registration and immediate log-in (jwt generation) 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            // User already exists
            var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                _logger.LogInformation($"WebApi register. User {registerDTO.Email} already registered!");
                return BadRequest(new MessageDTO("User already registered!"));
            }
            
            // Create new user
            var newUser = new DomainIdentity.AppUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email.ToLower().Split('@')[0],
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName
            };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
            // Check creation 
            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api register. Could not register user {registerDTO.Email}!");
                var errors = result.Errors.Select(error => error.Description).ToList();
                return BadRequest(new MessageDTO() {Messages = errors});
            }

            _logger.LogInformation($"Web-Api register. User {registerDTO.Email} registered!");
            
            // Find newly created user
            var newRegisteredUser = await _userManager.FindByEmailAsync(newUser.Email);
            if (newRegisteredUser == null)
            {
                _logger.LogInformation($"User {newUser.Email} not found after creation!");
                return NotFound(new MessageDTO("User not found after creation!"));
            }

            // Log new user in
            return await LogIn(newRegisteredUser);
        }

        private async Task<IActionResult> LogIn(DomainIdentity.AppUser appUser)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); // get the User analog
            var jwt = IdentityExtensions.GenerateJWT(
                claimsPrincipal.Claims
                    .Append(new Claim(ClaimTypes.GivenName, appUser.FirstName))
                    .Append(new Claim(ClaimTypes.Surname, appUser.LastName)),
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays"));

            _logger.LogInformation($"Web-Api login. Token generated for user {appUser.Email}");
            return Ok(new JwtResponseDTO()
            
            {
                Token = jwt,
                Status = $"User {appUser.Email} logged in.",
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName
            });
        }
    }
}