// using Domain.Identity;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
//
// namespace WebApp.ApiControllers.Identity
// {
//     [ApiController]
//     [Route("api/admin/[controller]")]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
//     public class AccountRolesController : ControllerBase
//     {
//         private readonly UserManager<AppUser> _userManager;
//         private readonly RoleManager<AppUser> _roleManager;
//         private readonly IConfiguration _configuration;
//
//         public AccountRolesController(UserManager<AppUser> userManager, RoleManager<AppUser> roleManager,
//             IConfiguration configuration)
//         {
//             _userManager = userManager;
//             _roleManager = roleManager;
//             _configuration = configuration;
//         }
//
//         [HttpGet]
//         public async void GetAllUsers()
//         {
//             
//         }
//
//     }
// }