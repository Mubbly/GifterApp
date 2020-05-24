// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Contracts.BLL.App;
// using Extensions;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using PublicApi.DTO.v1.Mappers;
// using V1DTO = PublicApi.DTO.v1;
//
// namespace WebApp.ApiControllers._1._0
// {
//     [ApiController]
//     [ApiVersion("1.0")]
//     [Route("api/v{version:apiVersion}/[controller]")]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//     public class UserPermissionsController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly ILogger<UserPermissionsController> _logger;
//         private readonly UserPermissionMapper _mapper = new UserPermissionMapper();
//
//         public UserPermissionsController(IAppBLL appBLL, ILogger<UserPermissionsController> logger)
//         {
//             _bll = appBLL;
//             _logger = logger;
//         }
//         
//                 // GET: api/UserPermissions
//         /// <summary>
//         ///     Get all UserPermissions
//         /// </summary>
//         /// <returns>List of UserPermissions</returns>
//         [HttpGet]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<IEnumerable<V1DTO.UserPermissionDTO>>> GetUserPermissions()
//         {
//             return Ok((await _bll.UserPermissions.GetAllAsync()).Select(e => _mapper.Map(e)));
//         }
//
//         // GET: api/UserPermissions/5
//         /// <summary>
//         ///     Get a single UserPermission
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns>UserPermission object</returns>
//         [HttpGet("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserPermissionDTO>> GetUserPermission(Guid id)
//         {
//             var userPermission = await _bll.UserPermissions.FirstOrDefaultAsync(id);
//             if (userPermission == null)
//             {
//                 return NotFound(new V1DTO.MessageDTO($"UserPermission with id {id} not found"));
//             }
//
//             return Ok(_mapper.Map(userPermission));
//         }
//
//         // PUT: api/UserPermissions/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Update UserPermission
//         /// </summary>
//         /// <param name="id"></param>
//         /// <param name="userPermissionDTO"></param>
//         /// <returns></returns>
//         [HttpPut("{id}")]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         public async Task<IActionResult> PutUserPermission(Guid id, V1DTO.UserPermissionDTO userPermissionDTO)
//         {
//             // Don't allow wrong data
//             if (id != userPermissionDTO.Id)
//             {
//                 return BadRequest(new V1DTO.MessageDTO("id and userPermission.id do not match"));
//             }
//             var userPermission = await _bll.UserPermissions.FirstOrDefaultAsync(userPermissionDTO.Id, User.UserGuidId());
//             if (userPermission == null)
//             {
//                 _logger.LogError($"EDIT. No such userPermission: {userPermissionDTO.Id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"No UserPermission found for id {id}"));
//             }
//             // Update existing userPermission
//             // userPermission.UserPermissionValue = userPermissionEditDTO.UserPermissionValue;
//             // userPermission.Comment = userPermissionEditDTO.Comment;
//             await _bll.UserPermissions.UpdateAsync(_mapper.Map(userPermissionDTO), User.UserId());
//
//             // Save to db
//             try
//             {
//                 await _bll.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!await _bll.UserPermissions.ExistsAsync(id, User.UserGuidId()))
//                 {
//                     _logger.LogError(
//                         $"EDIT. UserPermission does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
//                     return NotFound();
//                 }
//
//                 throw;
//             }
//             return NoContent();
//         }
//
//         // POST: api/UserPermissions
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Add new UserPermission
//         /// </summary>
//         /// <param name="userPermissionDTO"></param>
//         /// <returns></returns>
//         [HttpPost]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.UserPermissionDTO))]
//         public async Task<ActionResult<V1DTO.UserPermissionDTO>> PostUserPermission(V1DTO.UserPermissionDTO userPermissionDTO)
//         {
//             // Create userPermission
//             var bllEntity = _mapper.Map(userPermissionDTO);
//             _bll.UserPermissions.Add(bllEntity);
//             
//             // var userPermission = new UserPermission
//             // {
//             //     UserPermissionValue = userPermissionCreateDTO.UserPermissionValue,
//             //     Comment = userPermissionCreateDTO.Comment
//             // };
//
//             await _bll.SaveChangesAsync();
//
//             userPermissionDTO.Id = bllEntity.Id;
//             return CreatedAtAction(
//                 "GetUserPermission",
//                 new {id = userPermissionDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
//                 userPermissionDTO
//                 );
//         }
//
//         // DELETE: api/UserPermissions/5
//         /// <summary>
//         ///     Delete UserPermission
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//         [HttpDelete("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserPermissionDTO>> DeleteUserPermission(Guid id)
//         {
//             var userPermission = await _bll.UserPermissions.FirstOrDefaultAsync(id, User.UserGuidId());
//             if (userPermission == null)
//             {
//                 _logger.LogError($"DELETE. No such userPermission: {id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"UserPermission with id {id} not found"));
//             }
//             await _bll.UserPermissions.RemoveAsync(id);
//
//             await _bll.SaveChangesAsync();
//             return Ok(userPermission);
//         }
//
//         // // GET: api/UserPermissions
//         // [HttpGet]
//         // public async Task<ActionResult<IEnumerable<V1DTO.UserPermissionDTO>>> GetUserPermissions()
//         // {
//         //     return await _context.UserPermissions
//         //         .Select(up => new V1DTO.UserPermissionDTO()
//         //         {
//         //             Id = up.Id,
//         //             Comment = up.Comment,
//         //             From = up.From,
//         //             To = up.To,
//         //             PermissionId = up.PermissionId,
//         //             AppUserId = up.AppUserId
//         //         }).ToListAsync();
//         // }
//         //
//         // // GET: api/UserPermissions/5
//         // [HttpGet("{id}")]
//         // public async Task<ActionResult<V1DTO.UserPermissionDTO>> GetUserPermission(Guid id)
//         // {
//         //     var userPermission = await _context.UserPermissions
//         //         .Select(up => new V1DTO.UserPermissionDTO()
//         //         {
//         //             Id = up.Id,
//         //             Comment = up.Comment,
//         //             From = up.From,
//         //             To = up.To,
//         //             PermissionId = up.PermissionId,
//         //             AppUserId = up.AppUserId
//         //         }).SingleOrDefaultAsync();
//         //
//         //     if (userPermission == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     return userPermission;
//         // }
//         //
//         // // PUT: api/UserPermissions/5
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPut("{id}")]
//         // public async Task<IActionResult> PutUserPermission(Guid id, UserPermission userPermission)
//         // {
//         //     if (id != userPermission.Id)
//         //     {
//         //         return BadRequest();
//         //     }
//         //
//         //     _context.Entry(userPermission).State = EntityState.Modified;
//         //
//         //     try
//         //     {
//         //         await _context.SaveChangesAsync();
//         //     }
//         //     catch (DbUpdateConcurrencyException)
//         //     {
//         //         if (!UserPermissionExists(id))
//         //         {
//         //             return NotFound();
//         //         }
//         //         else
//         //         {
//         //             throw;
//         //         }
//         //     }
//         //
//         //     return NoContent();
//         // }
//         //
//         // // POST: api/UserPermissions
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPost]
//         // public async Task<ActionResult<UserPermission>> PostUserPermission(UserPermission userPermission)
//         // {
//         //     _context.UserPermissions.Add(userPermission);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return CreatedAtAction("GetUserPermission", new { id = userPermission.Id }, userPermission);
//         // }
//         //
//         // // DELETE: api/UserPermissions/5
//         // [HttpDelete("{id}")]
//         // public async Task<ActionResult<UserPermission>> DeleteUserPermission(Guid id)
//         // {
//         //     var userPermission = await _context.UserPermissions.FindAsync(id);
//         //     if (userPermission == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     _context.UserPermissions.Remove(userPermission);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return userPermission;
//         // }
//         //
//         // private bool UserPermissionExists(Guid id)
//         // {
//         //     return _context.UserPermissions.Any(e => e.Id == id);
//         // }
//     }
// }
