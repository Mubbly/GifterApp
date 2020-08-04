using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.App.DTO;
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
using DomainIdentity=Domain.App.Identity;

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
            
            // Save user activity
            appUser.LastActive = DateTime.Now;
            // appUser.IsActive = true;
            await _userManager.UpdateAsync(appUser);
            await _bll.SaveChangesAsync();

            // Log user in
            return await LogUserIn(appUser);
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
            
            // Create default profile with an empty wishlist
            try
            {
                _bll.Profiles.CreateDefaultProfile(newRegisteredUser.Id);
                await _bll.SaveChangesAsync();
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError($"Could not create default profile for new registered user {registerDTO.Email} - userId not provided", e);
            }
            
            // Check if user was invited by existing user
            var invitedUsers = (await _bll.InvitedUsers.GetAllAsync())
                .Where(i => i.Email == newRegisteredUser.Email)
                .ToList();
            if (invitedUsers != null && invitedUsers.Any())
            {
                foreach (var invitedUser in invitedUsers)
                {
                    // Update invitedUser to mark they have joined
                    invitedUser.HasJoined = true;
                    await _bll.InvitedUsers.UpdateAsync(invitedUser);
                    await _bll.SaveChangesAsync();

                    // Send notification to the invitor about their friend joining
                    var invitor = await _userManager.FindByIdAsync(invitedUser.InvitorUserId.ToString());
                    // TODO 
                }
            }
            // Save user activity
            newRegisteredUser.LastActive = DateTime.Now;
            await _userManager.UpdateAsync(newRegisteredUser);
            await _bll.SaveChangesAsync();
            
            // Log new user in
            return await LogUserIn(newRegisteredUser);
        }

        private async Task<IActionResult> LogUserIn(DomainIdentity.AppUser appUser)
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

        // /// <summary>
        // ///     Endpoint for user log-out (update activity related data)
        // /// </summary>
        // /// <param name="userId">Id of logged out user</param>
        // /// <returns></returns>
        // [HttpPost]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponseDTO))]
        // public async Task<ActionResult<AppUserDTO>> Logout([FromBody] Guid userId)
        // {
        //     // No user with such ID found
        //     var appUser = await _userManager.FindByIdAsync(userId.ToString());
        //     if (appUser == null)
        //     {
        //         _logger.LogInformation($"Web-Api login. User not found!");
        //         return NotFound(new MessageDTO("User not found!"));
        //     }
        //     // Save user activity
        //     appUser.LastActive = DateTime.Now;
        //     appUser.IsActive = false;
        //     await _userManager.UpdateAsync(appUser);
        //     await _bll.SaveChangesAsync();
        //
        //     var updatedData = new AppUserDTO()
        //     {
        //         LastActive = appUser.LastActive,
        //         IsActive = appUser.IsActive
        //     };
        //     // Send updated data back
        //     return Ok(updatedData);
        // }
    }
}