using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;
using DomainIdentity = Domain.App.Identity;

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
        private readonly ProfileMapper _mapper = new ProfileMapper();
        private readonly UserManager<DomainIdentity.AppUser> _userManager;
        private readonly AppUserMapper _userMapper = new AppUserMapper();
        private readonly ILogger<ProfilesController> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="appBLL"></param>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        public ProfilesController(IAppBLL appBLL, ILogger<ProfilesController> logger, UserManager<DomainIdentity.AppUser> userManager)
        {
            _bll = appBLL;
            _logger = logger;
            _userManager = userManager;
        }

        // ----------------------------- GET ONLY PROFILE TABLE DATA WITHOUT WISHLIST/GIFTS --------------------------

        // GET: api/Profiles/5/Data
        /// <summary>
        ///     Get a single profile - just the data without wishlist/gifts
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
        
        // GET: api/Profiles/Personal/Data
        /// <summary>
        ///     Get a single personal profile - just the data without wishlist/gifts
        /// </summary>
        /// <returns>Personal profile object</returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ProfileDTO>> GetPersonalProfile()
        {
            var personalProfile = await _bll.Profiles.GetPersonalAsync(User.UserGuidId());
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
        ///     Update profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
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
            // Update existing profile
            var profile = await _bll.Profiles.GetPersonalAsync(User.UserGuidId(), id);
            if (profile == null)
            {
                _logger.LogError($"EDIT. No such profile: {profileDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO("No profile found"));
            }

            var profileBLL = _mapper.Map(profileDTO);
            await _bll.Profiles.UpdateAsync(profileBLL, User.UserGuidId());

            // Save to db
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // --------------------------------- GET FULL PROFILE WITH USER DATA, WISHLISTS AND GIFTS --------------------------------------
        //
        //
        // // GET: api/Profiles/5
        // /// <summary>
        // ///     Get a single Profile
        // /// </summary>
        // /// <param name="id"></param>
        // /// <returns>Profile object</returns>
        // [HttpGet("{id}")]
        // [Produces("application/json")]
        // [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        // public async Task<ActionResult<V1DTO.ProfileDTO>> GetProfile(Guid id)
        // {
        //     var profile = await _bll.Profiles.FirstOrDefaultAsync(id);
        //     if (profile == null)
        //     {
        //         return NotFound(new V1DTO.MessageDTO($"Profile with id {id} not found"));
        //     }
        //
        //     return Ok(_mapper.Map(profile));
        // }
        //
        // // GET: api/Profiles/Personal
        // /// <summary>
        // ///     Get a single personal Profile
        // /// </summary>
        // /// <returns>Personal profile object</returns>
        // [HttpGet("personal")]
        // [Produces("application/json")]
        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        // public async Task<ActionResult<V1DTO.ProfileDTO>> GetPersonalProfile()
        // {
        //     var personalProfile = await _bll.Profiles.GetPersonalAsync(User.UserGuidId());
        //     if (personalProfile == null)
        //     {
        //         return NotFound(new V1DTO.MessageDTO($"Profile not found"));
        //     }
        //     if (personalProfile.AppUserId != User.UserGuidId())
        //     {
        //         return BadRequest(new V1DTO.MessageDTO($"Profile not found"));
        //     }
        //     
        //     return Ok(_mapper.Map(personalProfile));
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
        
        // // PUT: api/Profiles/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // /// <summary>
        // ///     Update profile
        // /// </summary>
        // /// <param name="id"></param>
        // /// <param name="profileDTO"></param>
        // /// <returns></returns>
        // [HttpPut("{id}")]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // public async Task<IActionResult> PutProfile(Guid id, V1DTO.ProfileDTO profileDTO)
        // {
        //     // TODO: Move to BLL
        //     // Don't allow wrong data
        //     if (id != profileDTO.Id)
        //     {
        //         return BadRequest(new V1DTO.MessageDTO("Cannot update this profile"));
        //     }
        //     // Update existing profile
        //     var profile = await _bll.Profiles.GetPersonalAsync(User.UserGuidId(), id);
        //     if (profile == null)
        //     {
        //         _logger.LogError($"EDIT. No such profile: {profileDTO.Id}, user: {User.UserGuidId()}");
        //         return NotFound(new V1DTO.MessageDTO($"No profile found for id {id}"));
        //     }
        //     await _bll.Profiles.UpdateAsync(_mapper.Map(profileDTO), User.UserId());
        //     
        //     // Update existing user
        //     var user = await _userManager.FindByIdAsync(User.UserGuidId().ToString());
        //     if (user == null || profileDTO.AppUser == null)
        //     {
        //         _logger.LogError($"PROFILE EDIT. No such user: {User.UserGuidId()}");
        //         return BadRequest(new V1DTO.MessageDTO("Cannot update this profile for this user"));
        //     }
        //     user.FirstName = profileDTO.AppUser.FirstName != null ? profileDTO.AppUser.FirstName : user.FirstName;
        //     user.LastName = profileDTO.AppUser.LastName != null ? profileDTO.AppUser.LastName : user.LastName;
        //     user.Email = profileDTO.AppUser.Email != null ? profileDTO.AppUser.Email : user.Email;
        //     await _userManager.UpdateAsync(user);
        //
        //     // Save to db
        //     await _bll.SaveChangesAsync();
        //     return NoContent();
        // }
    }
}
