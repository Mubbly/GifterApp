using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    ///     Profiles controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProfilesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ProfilesController> _logger;
        private readonly ProfileMapper _mapper = new ProfileMapper();

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="appBLL"></param>
        /// <param name="logger"></param>
        public ProfilesController(IAppBLL appBLL, ILogger<ProfilesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Profiles
        /// <summary>
        ///     Get all Profiles
        /// </summary>
        /// <returns>List of Profiles</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ProfileDTO>>> GetProfiles()
        {
            return Ok((await _bll.Profiles.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Profiles/5
        /// <summary>
        ///     Get a single Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Profile object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ProfileDTO>> GetProfile(Guid id)
        {
            var profile = await _bll.Profiles.FirstOrDefaultAsync(id);
            if (profile == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Profile with id {id} not found"));
            }

            return Ok(_mapper.Map(profile));
        }
        
        // GET: api/Profiles/Personal/ or api/Profiles/Personal/5
        /// <summary>
        ///     Get a single personal Profile
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns>Personal profile object</returns>
        [HttpGet("personal/{id?}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ProfileDTO>> GetPersonalProfile(Guid? profileId)
        {
            var personalProfile = await _bll.Profiles.GetPersonalAsync(User.UserGuidId(), profileId);
            if (personalProfile == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Profile not found"));
            }
            if (personalProfile.AppUserId != User.UserGuidId())
            {
                return BadRequest(new V1DTO.MessageDTO($"Profile not found"));
            }
            
            return Ok(_mapper.Map(personalProfile));
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutProfile(Guid id, V1DTO.ProfileDTO profileDTO)
        {
            // Don't allow wrong data
            if (id != profileDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("Cannot update this profile"));
            }
            var profile = await _bll.Profiles.FirstOrDefaultAsync(profileDTO.Id, User.UserGuidId());
            if (profile == null)
            {
                _logger.LogError($"EDIT. No such profile: {profileDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No profile found for id {id}"));
            }
            // Update existing profile
            // profile.ProfileValue = profileEditDTO.ProfileValue;
            // profile.Comment = profileEditDTO.Comment;
            await _bll.Profiles.UpdateAsync(_mapper.Map(profileDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Profiles.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Profile does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // // POST: api/Profiles
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // /// <summary>
        // ///     Add new Profile
        // /// </summary>
        // /// <param name="profileDTO"></param>
        // /// <returns></returns>
        // [HttpPost]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ProfileDTO))]
        // public async Task<ActionResult<V1DTO.ProfileDTO>> PostProfile(V1DTO.ProfileDTO profileDTO)
        // {
        //     // Create profile
        //     var bllEntity = _mapper.Map(profileDTO);
        //     _bll.Profiles.Add(bllEntity);
        //     
        //     // var profile = new Profile
        //     // {
        //     //     ProfileValue = profileCreateDTO.ProfileValue,
        //     //     Comment = profileCreateDTO.Comment
        //     // };
        //
        //     await _bll.SaveChangesAsync();
        //
        //     profileDTO.Id = bllEntity.Id;
        //     return CreatedAtAction(
        //         "GetProfile",
        //         new {id = profileDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
        //         profileDTO
        //         );
        // }

        // // DELETE: api/Profiles/5
        // /// <summary>
        // ///     Delete Profile
        // /// </summary>
        // /// <param name="id"></param>
        // /// <returns></returns>
        // [HttpDelete("{id}")]
        // [Produces("application/json")]
        // [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        // public async Task<ActionResult<V1DTO.ProfileDTO>> DeleteProfile(Guid id)
        // {
        //     var profile = await _bll.Profiles.FirstOrDefaultAsync(id, User.UserGuidId());
        //     if (profile == null)
        //     {
        //         _logger.LogError($"DELETE. No such profile: {id}, user: {User.UserGuidId()}");
        //         return NotFound(new V1DTO.MessageDTO($"Profile with id {id} not found"));
        //     }
        //     await _bll.Profiles.RemoveAsync(id);
        //
        //     await _bll.SaveChangesAsync();
        //     return Ok(profile);
        // }

        // // GET: api/Profiles
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.ProfileDTO>>> GetProfiles()
        // {
        //     return await _context.Profiles
        //         .Select(p => new V1DTO.ProfileDTO() 
        //         {
        //             Id = p.Id,
        //             Age = p.Age,
        //             Bio = p.Bio,
        //             Gender = p.Gender,
        //             IsPrivate = p.IsPrivate,
        //             ProfilePicture = p.ProfilePicture,
        //             WishlistId = p.WishlistId,
        //             AppUserId = p.AppUserId,
        //             UserProfilesCount = p.UserProfiles.Count
        //         }).ToListAsync();
        // }
        //
        // // GET: api/Profiles/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.ProfileDTO>> GetProfile(Guid id)
        // {
        //     var profile = await _context.Profiles
        //         .Select(p => new V1DTO.ProfileDTO() 
        //         {
        //             Id = p.Id,
        //             Age = p.Age,
        //             Bio = p.Bio,
        //             Gender = p.Gender,
        //             IsPrivate = p.IsPrivate,
        //             ProfilePicture = p.ProfilePicture,
        //             WishlistId = p.WishlistId,
        //             AppUserId = p.AppUserId,
        //             UserProfilesCount = p.UserProfiles.Count
        //         }).SingleOrDefaultAsync();
        //
        //     if (profile == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return profile;
        // }
        //
        // // PUT: api/Profiles/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutProfile(Guid id, Profile profile)
        // {
        //     if (id != profile.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(profile).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ProfileExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // POST: api/Profiles
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        // {
        //     _context.Profiles.Add(profile);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        // }
        //
        // // DELETE: api/Profiles/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Profile>> DeleteProfile(Guid id)
        // {
        //     var profile = await _context.Profiles.FindAsync(id);
        //     if (profile == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Profiles.Remove(profile);
        //     await _context.SaveChangesAsync();
        //
        //     return profile;
        // }
        //
        // private bool ProfileExists(Guid id)
        // {
        //     return _context.Profiles.Any(e => e.Id == id);
        // }
    }
}
