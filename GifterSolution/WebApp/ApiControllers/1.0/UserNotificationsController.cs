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
//     public class UserNotificationsController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly ILogger<UserNotificationsController> _logger;
//         private readonly UserNotificationMapper _mapper = new UserNotificationMapper();
//
//         public UserNotificationsController(IAppBLL appBLL, ILogger<UserNotificationsController> logger)
//         {
//             _bll = appBLL;
//             _logger = logger;
//         }
//         
//                 // GET: api/UserNotifications
//         /// <summary>
//         ///     Get all UserNotifications
//         /// </summary>
//         /// <returns>List of UserNotifications</returns>
//         [HttpGet]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<IEnumerable<V1DTO.UserNotificationDTO>>> GetUserNotifications()
//         {
//             return Ok((await _bll.UserNotifications.GetAllAsync()).Select(e => _mapper.Map(e)));
//         }
//
//         // GET: api/UserNotifications/5
//         /// <summary>
//         ///     Get a single UserNotification
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns>UserNotification object</returns>
//         [HttpGet("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserNotificationDTO>> GetUserNotification(Guid id)
//         {
//             var userNotification = await _bll.UserNotifications.FirstOrDefaultAsync(id);
//             if (userNotification == null)
//             {
//                 return NotFound(new V1DTO.MessageDTO($"UserNotification with id {id} not found"));
//             }
//
//             return Ok(_mapper.Map(userNotification));
//         }
//
//         // PUT: api/UserNotifications/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Update UserNotification
//         /// </summary>
//         /// <param name="id"></param>
//         /// <param name="userNotificationDTO"></param>
//         /// <returns></returns>
//         [HttpPut("{id}")]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         public async Task<IActionResult> PutUserNotification(Guid id, V1DTO.UserNotificationDTO userNotificationDTO)
//         {
//             // Don't allow wrong data
//             if (id != userNotificationDTO.Id)
//             {
//                 return BadRequest(new V1DTO.MessageDTO("id and userNotification.id do not match"));
//             }
//             var userNotification = await _bll.UserNotifications.FirstOrDefaultAsync(userNotificationDTO.Id, User.UserGuidId());
//             if (userNotification == null)
//             {
//                 _logger.LogError($"EDIT. No such userNotification: {userNotificationDTO.Id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"No UserNotification found for id {id}"));
//             }
//             // Update existing userNotification
//             // userNotification.UserNotificationValue = userNotificationEditDTO.UserNotificationValue;
//             // userNotification.Comment = userNotificationEditDTO.Comment;
//             await _bll.UserNotifications.UpdateAsync(_mapper.Map(userNotificationDTO), User.UserId());
//
//             // Save to db
//             try
//             {
//                 await _bll.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!await _bll.UserNotifications.ExistsAsync(id, User.UserGuidId()))
//                 {
//                     _logger.LogError(
//                         $"EDIT. UserNotification does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
//                     return NotFound();
//                 }
//
//                 throw;
//             }
//             return NoContent();
//         }
//
//         // POST: api/UserNotifications
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Add new UserNotification
//         /// </summary>
//         /// <param name="userNotificationDTO"></param>
//         /// <returns></returns>
//         [HttpPost]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.UserNotificationDTO))]
//         public async Task<ActionResult<V1DTO.UserNotificationDTO>> PostUserNotification(V1DTO.UserNotificationDTO userNotificationDTO)
//         {
//             // Create userNotification
//             var bllEntity = _mapper.Map(userNotificationDTO);
//             _bll.UserNotifications.Add(bllEntity);
//             
//             // var userNotification = new UserNotification
//             // {
//             //     UserNotificationValue = userNotificationCreateDTO.UserNotificationValue,
//             //     Comment = userNotificationCreateDTO.Comment
//             // };
//
//             await _bll.SaveChangesAsync();
//
//             userNotificationDTO.Id = bllEntity.Id;
//             return CreatedAtAction(
//                 "GetUserNotification",
//                 new {id = userNotificationDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
//                 userNotificationDTO
//                 );
//         }
//
//         // DELETE: api/UserNotifications/5
//         /// <summary>
//         ///     Delete UserNotification
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//         [HttpDelete("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserNotificationDTO>> DeleteUserNotification(Guid id)
//         {
//             var userNotification = await _bll.UserNotifications.FirstOrDefaultAsync(id, User.UserGuidId());
//             if (userNotification == null)
//             {
//                 _logger.LogError($"DELETE. No such userNotification: {id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"UserNotification with id {id} not found"));
//             }
//             await _bll.UserNotifications.RemoveAsync(id);
//
//             await _bll.SaveChangesAsync();
//             return Ok(userNotification);
//         }
//
//         // // GET: api/UserNotifications
//         // [HttpGet]
//         // public async Task<ActionResult<IEnumerable<V1DTO.UserNotificationDTO>>> GetUserNotifications()
//         // {
//         //     return await _context.UserNotifications
//         //         .Select(un => new V1DTO.UserNotificationDTO()
//         //         {
//         //             Id = un.Id,
//         //             Comment = un.Comment,
//         //             IsActive = un.IsActive,
//         //             IsDisabled = un.IsDisabled,
//         //             LastNotified = un.LastNotified,
//         //             NotificationId = un.NotificationId,
//         //             RenotifyAt = un.RenotifyAt,
//         //             AppUserId = un.AppUserId
//         //         }).ToListAsync();
//         // }
//         //
//         // // GET: api/UserNotifications/5
//         // [HttpGet("{id}")]
//         // public async Task<ActionResult<V1DTO.UserNotificationDTO>> GetUserNotification(Guid id)
//         // {
//         //     var userNotification = await _context.UserNotifications
//         //         .Select(un => new V1DTO.UserNotificationDTO()
//         //         {
//         //             Id = un.Id,
//         //             Comment = un.Comment,
//         //             IsActive = un.IsActive,
//         //             IsDisabled = un.IsDisabled,
//         //             LastNotified = un.LastNotified,
//         //             NotificationId = un.NotificationId,
//         //             RenotifyAt = un.RenotifyAt,
//         //             AppUserId = un.AppUserId
//         //         }).SingleOrDefaultAsync();
//         //
//         //     if (userNotification == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     return userNotification;
//         // }
//         //
//         // // PUT: api/UserNotifications/5
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPut("{id}")]
//         // public async Task<IActionResult> PutUserNotification(Guid id, UserNotification userNotification)
//         // {
//         //     if (id != userNotification.Id)
//         //     {
//         //         return BadRequest();
//         //     }
//         //
//         //     _context.Entry(userNotification).State = EntityState.Modified;
//         //
//         //     try
//         //     {
//         //         await _context.SaveChangesAsync();
//         //     }
//         //     catch (DbUpdateConcurrencyException)
//         //     {
//         //         if (!UserNotificationExists(id))
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
//         // // POST: api/UserNotifications
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPost]
//         // public async Task<ActionResult<UserNotification>> PostUserNotification(UserNotification userNotification)
//         // {
//         //     _context.UserNotifications.Add(userNotification);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return CreatedAtAction("GetUserNotification", new { id = userNotification.Id }, userNotification);
//         // }
//         //
//         // // DELETE: api/UserNotifications/5
//         // [HttpDelete("{id}")]
//         // public async Task<ActionResult<UserNotification>> DeleteUserNotification(Guid id)
//         // {
//         //     var userNotification = await _context.UserNotifications.FindAsync(id);
//         //     if (userNotification == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     _context.UserNotifications.Remove(userNotification);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return userNotification;
//         // }
//         //
//         // private bool UserNotificationExists(Guid id)
//         // {
//         //     return _context.UserNotifications.Any(e => e.Id == id);
//         // }
//     }
// }
