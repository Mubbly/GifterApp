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
        
        // GET: api/Friendships/5
        /// <summary>
        ///     Get user confirmed Friendships. Pending ones are always personal.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of confirmed Friendships for given user</returns>
        [HttpGet("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipResponseDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipResponseDTO>>> GetUserFriendships(Guid userId)
        {
            var personalFriendships = await _bll.Friendships.GetAllConfirmedForUserAsync(userId);
            return Ok(personalFriendships.Select(e => _mapper.MapFriendshipResponseToDTO(e)));
        }
        
        // GET: api/Friendships/Personal
        /// <summary>
        ///     Get all personal Friendships - both pending and confirmed.
        /// </summary>
        /// <returns>List of all personal Friendships</returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipResponseDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipResponseDTO>>> GetPersonalFriendships()
        {
            var friendships = await _bll.Friendships.GetAllForUserAsync(User.UserGuidId());
            return Ok(friendships.Select(e => _mapper.MapFriendshipResponseToDTO(e)));
        }

        // GET: api/Friendships/Personal/Confirmed
        /// <summary>
        ///     Get personal confirmed Friendships
        /// </summary>
        /// <returns>List of confirmed personal Friendships</returns>
        [HttpGet("personal/confirmed")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipResponseDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipResponseDTO>>> GetPersonalConfirmedFriendships()
        {
            var personalFriendships = await _bll.Friendships.GetAllConfirmedForUserAsync(User.UserGuidId());
            return Ok(personalFriendships.Select(e => _mapper.MapFriendshipResponseToDTO(e)));
        }

        // GET: api/Friendships/Personal/Pending/Sent
        /// <summary>
        ///     Get personal pending Friendships that the user sent
        /// </summary>
        /// <returns>List of pending personal Friendships</returns>
        [HttpGet("personal/pending/sent")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipResponseDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipResponseDTO>>> GetPersonalSentPendingFriendships()
        {
            var pendingFriendships = await _bll.Friendships.GetAllPendingForUserAsync(User.UserGuidId(), true);
            return Ok(pendingFriendships.Select(e => _mapper.MapFriendshipResponseToDTO(e)));
        }
        
        // GET: api/Friendships/Personal/Pending/Received
        /// <summary>
        ///     Get personal pending Friendships that the user received
        /// </summary>
        /// <returns>List of pending personal Friendships</returns>
        [HttpGet("personal/pending/received")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipResponseDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipResponseDTO>>> GetPersonalReceivedPendingFriendships()
        {
            var pendingFriendships = await _bll.Friendships.GetAllPendingForUserAsync(User.UserGuidId(), false);
            return Ok(pendingFriendships.Select(e => _mapper.MapFriendshipResponseToDTO(e)));
        }
        
        // GET: api/Friendships/Personal/5
        /// <summary>
        ///     Get a single personal Friendship - no matter the status (pending or confirmed)
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("personal/{friendUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.GetForUserAsync(User.UserGuidId(), friendUserId);
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.MapFriendshipResponseToDTO(friendship));
        }

        // GET: api/Friendships/Personal/Confirmed/5
        /// <summary>
        ///     Get a single personal confirmed Friendship
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("personal/confirmed/{friendUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetConfirmedFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.GetForUserConfirmedAsync(User.UserGuidId(), friendUserId);
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.MapFriendshipResponseToDTO(friendship));
        }
        
        // GET: api/Friendships/Personal/Pending/5
        /// <summary>
        ///     Get a single personal pending Friendship
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("personal/pending/{friendUserId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetPendingFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.GetForUserPendingAsync(User.UserGuidId(), friendUserId);
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.MapFriendshipResponseToDTO(friendship));
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
            // Find pending friendship, don't allow if not found
            // // TODO: Change AppUser2Id name to FriendId in DTO
            var friendship = await _bll.Friendships.GetForUserPendingAsync(User.UserGuidId(), friendshipDTO.AppUser2Id);
            if (friendship == null)
            {
                _logger.LogError($"EDIT. No such friendship: {friendshipDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Friendship found for id {id}"));
            }
            // Don't allow if current user has no rights to confirm this friendship (not the addressee!)
            // if (friendship.AppUser2Id != User.UserGuidId())
            // {
            //     return BadRequest(new V1DTO.MessageDTO($"User {User.UserGuidId().ToString()} cannot confirm this friendship"));
            // }
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
            // Don't allow creating a confirmed friendship (only pending ones can be created)
            if (friendshipDTO.IsConfirmed)
            {
                return BadRequest(new V1DTO.MessageDTO($"Cannot add already confirmed friendship {friendshipDTO.Id}"));
            }
            // Don't allow re-creating an existing friendship
            var existingFriendship = await _bll.Friendships.GetForUserConfirmedAsync(User.UserGuidId(),friendshipDTO.AppUser2Id);
            var pendingFriendship = await _bll.Friendships.GetForUserPendingAsync(User.UserGuidId(),friendshipDTO.AppUser2Id);
            if (existingFriendship != null || pendingFriendship != null)
            {
                var friendshipId = existingFriendship?.Id ?? pendingFriendship.Id;
                return BadRequest(new V1DTO.MessageDTO($"Cannot add already existing friendship {friendshipId}"));
            }
            
            // Create pending friendship
            var bllEntity = _mapper.Map(friendshipDTO);
            await _bll.Friendships.Add(bllEntity, User.UserGuidId());
            await _bll.SaveChangesAsync();

            friendshipDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetConfirmedFriendship",
                new {id = friendshipDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                friendshipDTO
            );
        }

        // DELETE: api/Friendships/5
        /// <summary>
        ///     Delete personal friendship - remove pending request or unfriend an existing friend
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpDelete("{friendId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> DeleteFriendship(Guid friendId)
        {
            var friendship = await _bll.Friendships.GetForUserAsync(User.UserGuidId(), friendId);

            // No pending or confirmed friendship found
            if (friendship == null)
            {
                _logger.LogError($"DELETE. No such friendship: {friendId.ToString()}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Friendship with id {friendId.ToString()} not found"));
            }
            
            // Pending or confirmed relationship found - delete it
            await _bll.Friendships.RemoveAsync(friendship.Id);
            await _bll.SaveChangesAsync();
            return Ok(friendship);
        }
    }
}
