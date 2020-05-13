// using Domain.App.Identity;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
//
// namespace WebApp.ApiControllers._1._0.Identity
// {
//     /// <summary>
//     /// 
//     /// </summary>
//     [ApiController]
//     [ApiVersion("1.0")]
//     [Route("api/v{version:apiVersion}/[controller]/[action]")]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
//     public class AccountRolesController : ControllerBase
//     {
//         private readonly UserManager<AppUser> _userManager;
//         private readonly RoleManager<AppUser> _roleManager;
//         private readonly IConfiguration _configuration;
//
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="userManager"></param>
//         /// <param name="roleManager"></param>
//         /// <param name="configuration"></param>
//         public AccountRolesController(UserManager<AppUser> userManager, RoleManager<AppUser> roleManager,
//             IConfiguration configuration)
//         {
//             _userManager = userManager;
//             _roleManager = roleManager;
//             _configuration = configuration;
//         }
//
//         /// <summary>
//         /// 
//         /// </summary>
//         [HttpGet]
//         public async void GetAllUsers()
//         {
//             
//         }
//
//     }
// }
//
