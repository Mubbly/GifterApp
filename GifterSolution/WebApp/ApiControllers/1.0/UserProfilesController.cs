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
//     public class UserProfilesController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly ILogger<UserProfilesController> _logger;
//         private readonly UserProfileMapper _mapper = new UserProfileMapper();
//
//         public UserProfilesController(IAppBLL appBLL, ILogger<UserProfilesController> logger)
//         {
//             _bll = appBLL;
//             _logger = logger;
//         }
//         
//                 // GET: api/UserProfiles
//         /// <summary>
//         ///     Get all UserProfiles
//         /// </summary>
//         /// <returns>List of UserProfiles</returns>
//         [HttpGet]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<IEnumerable<V1DTO.UserProfileDTO>>> GetUserProfiles()
//         {
//             return Ok((await _bll.UserProfiles.GetAllAsync()).Select(e => _mapper.Map(e)));
//         }
//
//         // GET: api/UserProfiles/5
//         /// <summary>
//         ///     Get a single UserProfile
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns>UserProfile object</returns>
//         [HttpGet("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserProfileDTO>> GetUserProfile(Guid id)
//         {
//             var userProfile = await _bll.UserProfiles.FirstOrDefaultAsync(id);
//             if (userProfile == null)
//             {
//                 return NotFound(new V1DTO.MessageDTO($"UserProfile with id {id} not found"));
//             }
//
//             return Ok(_mapper.Map(userProfile));
//         }
//
//         // PUT: api/UserProfiles/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Update UserProfile
//         /// </summary>
//         /// <param name="id"></param>
//         /// <param name="userProfileDTO"></param>
//         /// <returns></returns>
//         [HttpPut("{id}")]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         public async Task<IActionResult> PutUserProfile(Guid id, V1DTO.UserProfileDTO userProfileDTO)
//         {
//             // Don't allow wrong data
//             if (id != userProfileDTO.Id)
//             {
//                 return BadRequest(new V1DTO.MessageDTO("id and userProfile.id do not match"));
//             }
//             var userProfile = await _bll.UserProfiles.FirstOrDefaultAsync(userProfileDTO.Id, User.UserGuidId());
//             if (userProfile == null)
//             {
//                 _logger.LogError($"EDIT. No such userProfile: {userProfileDTO.Id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"No UserProfile found for id {id}"));
//             }
//             // Update existing userProfile
//             // userProfile.UserProfileValue = userProfileEditDTO.UserProfileValue;
//             // userProfile.Comment = userProfileEditDTO.Comment;
//             await _bll.UserProfiles.UpdateAsync(_mapper.Map(userProfileDTO), User.UserId());
//
//             // Save to db
//             try
//             {
//                 await _bll.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!await _bll.UserProfiles.ExistsAsync(id, User.UserGuidId()))
//                 {
//                     _logger.LogError(
//                         $"EDIT. UserProfile does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
//                     return NotFound();
//                 }
//
//                 throw;
//             }
//             return NoContent();
//         }
//
//         // POST: api/UserProfiles
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Add new UserProfile
//         /// </summary>
//         /// <param name="userProfileDTO"></param>
//         /// <returns></returns>
//         [HttpPost]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.UserProfileDTO))]
//         public async Task<ActionResult<V1DTO.UserProfileDTO>> PostUserProfile(V1DTO.UserProfileDTO userProfileDTO)
//         {
//             // Create userProfile
//             var bllEntity = _mapper.Map(userProfileDTO);
//             _bll.UserProfiles.Add(bllEntity);
//             
//             // var userProfile = new UserProfile
//             // {
//             //     UserProfileValue = userProfileCreateDTO.UserProfileValue,
//             //     Comment = userProfileCreateDTO.Comment
//             // };
//
//             await _bll.SaveChangesAsync();
//
//             userProfileDTO.Id = bllEntity.Id;
//             return CreatedAtAction(
//                 "GetUserProfile",
//                 new {id = userProfileDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
//                 userProfileDTO
//                 );
//         }
//
//         // DELETE: api/UserProfiles/5
//         /// <summary>
//         ///     Delete UserProfile
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//         [HttpDelete("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.UserProfileDTO>> DeleteUserProfile(Guid id)
//         {
//             var userProfile = await _bll.UserProfiles.FirstOrDefaultAsync(id, User.UserGuidId());
//             if (userProfile == null)
//             {
//                 _logger.LogError($"DELETE. No such userProfile: {id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"UserProfile with id {id} not found"));
//             }
//             await _bll.UserProfiles.RemoveAsync(id);
//
//             await _bll.SaveChangesAsync();
//             return Ok(userProfile);
//         }
//
//         // // GET: api/UserProfiles
//         // [HttpGet]
//         // public async Task<ActionResult<IEnumerable<V1DTO.UserProfileDTO>>> GetUserProfiles()
//         // {
//         //     return await _context.UserProfiles
//         //         .Select(up => new V1DTO.UserProfileDTO()
//         //         {
//         //             Id = up.Id,
//         //             Comment = up.Comment,
//         //             ProfileId = up.ProfileId,
//         //             AppUserId = up.AppUserId
//         //         }).ToListAsync();
//         // }
//         //
//         // // GET: api/UserProfiles/5
//         // [HttpGet("{id}")]
//         // public async Task<ActionResult<V1DTO.UserProfileDTO>> GetUserProfile(Guid id)
//         // {
//         //     var userProfile = await _context.UserProfiles
//         //         .Select(up => new V1DTO.UserProfileDTO()
//         //         {
//         //             Id = up.Id,
//         //             Comment = up.Comment,
//         //             ProfileId = up.ProfileId,
//         //             AppUserId = up.AppUserId
//         //         }).SingleOrDefaultAsync();
//         //
//         //     if (userProfile == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     return userProfile;
//         // }
//         //
//         // // PUT: api/UserProfiles/5
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPut("{id}")]
//         // public async Task<IActionResult> PutUserProfile(Guid id, UserProfile userProfile)
//         // {
//         //     if (id != userProfile.Id)
//         //     {
//         //         return BadRequest();
//         //     }
//         //
//         //     _context.Entry(userProfile).State = EntityState.Modified;
//         //
//         //     try
//         //     {
//         //         await _context.SaveChangesAsync();
//         //     }
//         //     catch (DbUpdateConcurrencyException)
//         //     {
//         //         if (!UserProfileExists(id))
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
//         // // POST: api/UserProfiles
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPost]
//         // public async Task<ActionResult<UserProfile>> PostUserProfile(UserProfile userProfile)
//         // {
//         //     _context.UserProfiles.Add(userProfile);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
//         // }
//         //
//         // // DELETE: api/UserProfiles/5
//         // [HttpDelete("{id}")]
//         // public async Task<ActionResult<UserProfile>> DeleteUserProfile(Guid id)
//         // {
//         //     var userProfile = await _context.UserProfiles.FindAsync(id);
//         //     if (userProfile == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     _context.UserProfiles.Remove(userProfile);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return userProfile;
//         // }
//         //
//         // private bool UserProfileExists(Guid id)
//         // {
//         //     return _context.UserProfiles.Any(e => e.Id == id);
//         // }
//     }
// }
