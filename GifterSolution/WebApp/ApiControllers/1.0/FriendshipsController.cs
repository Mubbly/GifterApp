using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class FriendshipsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<FriendshipsController> _logger;
        private readonly FriendshipMapper _mapper = new FriendshipMapper();

        public FriendshipsController(IAppBLL appBLL, ILogger<FriendshipsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Friendships/Personal
        /// <summary>
        ///     Get user confirmed Friendships
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Friendships</returns>
        [HttpGet("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetUserFriendships(Guid userId)
        {
            var personalFriendships = await _bll.Friendships.GetAllConfirmedForUserAsync(userId);
            return Ok(personalFriendships.Select(e => _mapper.Map(e)));
        }

        // GET: api/Friendships/Personal
        /// <summary>
        ///     Get personal confirmed Friendships
        /// </summary>
        /// <returns>List of Friendships</returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetPersonalFriendships()
        {
            var personalFriendships = await _bll.Friendships.GetAllConfirmedForUserAsync(User.UserGuidId());
            return Ok(personalFriendships.Select(e => _mapper.Map(e)));
        }
        
        // GET: api/Friendships/Personal/Pending
        /// <summary>
        ///     Get personal pending Friendships
        /// </summary>
        /// <returns>List of Friendships</returns>
        [HttpGet("personal/pending")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetPersonalPendingFriendships()
        {
            var pendingFriendships = await _bll.Friendships.GetAllPendingForUserAsync(User.UserGuidId());
            return Ok(pendingFriendships.Select(e => _mapper.Map(e)));
        }

        // GET: api/Friendships/Personal/5
        /// <summary>
        ///     Get a single personal confirmed Friendship
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("personal/{friendUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetConfirmedFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.GetConfirmedForUserAsync(User.UserGuidId(), friendUserId);
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.Map(friendship));
        }
        
        // GET: api/Friendships/Personal/Pending/5
        /// <summary>
        ///     Get a single personal pending Friendship
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("personal/pending/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetPendingFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.GetPendingForUserAsync(User.UserGuidId(), friendUserId);
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.Map(friendship));
        }

        // PUT: api/Friendships/personal/pending/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Confirm friendship (from pending status to confirmed)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendshipDTO"></param>
        /// <returns></returns>
        [HttpPut("personal/pending/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutPersonalFriendship(Guid id, V1DTO.FriendshipDTO friendshipDTO)
        {
            // Don't allow wrong data
            if (id != friendshipDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and friendship.id do not match"));
            }
            // Don't allow if current user has no rights to confirm this friendship (not the addressee)
            if (friendshipDTO.AppUser2Id != User.UserGuidId())
            {
                return BadRequest(new V1DTO.MessageDTO($"User {User.UserGuidId().ToString()} cannot confirm this friendship"));
            }
            // Find friendship, don't allow if not found
            var friendship = await _bll.Friendships.GetPendingForUserAsync(User.UserGuidId(), friendshipDTO.AppUser1Id);
            if (friendship == null)
            {
                _logger.LogError($"EDIT. No such friendship: {friendshipDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Friendship found for id {id}"));
            }
            // Update existing friendship (status to confirmed)
            await _bll.Friendships.UpdateAsync(_mapper.Map(friendshipDTO), User.UserGuidId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }
        
        // POST: api/Friendships/pending
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new pending Friendship - when request is sent but not yet accepted
        /// </summary>
        /// <param name="friendshipDTO"></param>
        /// <returns></returns>
        [HttpPost("personal/pending")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.FriendshipDTO))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> PostPersonalFriendship(V1DTO.FriendshipDTO friendshipDTO)
        {
            // Don't allow creating friendships where current user is not the requester
            if (friendshipDTO.AppUser1Id != User.UserGuidId())
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot add friendship {friendshipDTO.Id}"));
            }
            // Don't allow creating an already confirmed friendship
            if (friendshipDTO.IsConfirmed)
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot add already confirmed friendship {friendshipDTO.Id}"));
            }
            // Don't allow re-creating an existing friendship
            var existingFriendship = await _bll.Friendships.GetConfirmedForUserAsync(User.UserGuidId(),friendshipDTO.AppUser2Id);
            var pendingFriendship = await _bll.Friendships.GetPendingForUserAsync(User.UserGuidId(),friendshipDTO.AppUser2Id);
            if (existingFriendship != null || pendingFriendship != null)
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot add already existing friendship {friendshipDTO.Id}"));
            }
            
            // Create pending friendship
            var bllEntity = _mapper.Map(friendshipDTO);
            _bll.Friendships.Add(bllEntity);
            await _bll.SaveChangesAsync();

            friendshipDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetConfirmedFriendship",
                new {id = friendshipDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                friendshipDTO
            );
        }

        // DELETE: api/Friendships/personal/5
        /// <summary>
        ///     Delete friendship - remove pending request or unfriend an existing friend
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("personal/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> DeletePersonalFriendship(Guid id)
        {
            var friendship = await _bll.Friendships.FirstOrDefaultAsync(id, User.UserGuidId());
            if (friendship == null)
            {
                _logger.LogError($"DELETE. No such friendship: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Friendship with id {id} not found"));
            }
            await _bll.Friendships.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(friendship);
        }
    }
}
