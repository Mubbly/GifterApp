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
        ///     Get personal accepted Friendships
        /// </summary>
        /// <returns>List of Friendships</returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetPersonalFriendships()
        {
            var personalFriendships = await _bll.Friendships.GetAllPersonalAsync(User.UserGuidId(), true);
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
            var pendingFriendships = await _bll.Friendships.GetAllPersonalAsync(User.UserGuidId(), false);
            return Ok(pendingFriendships.Select(e => _mapper.Map(e)));
        }

        // GET: api/Friendships/5
        /// <summary>
        ///     Get a single Friendship
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetFriendship(Guid friendUserId)
        {
            var friendship = await _bll.Friendships.FirstOrDefaultAsync(friendUserId, User.UserGuidId());
            if (friendship == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with user {friendUserId} not found"));
            }
            return Ok(_mapper.Map(friendship));
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Change friendship (from one state to another - pending or confirmed)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendshipDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutFriendship(Guid id, V1DTO.FriendshipDTO friendshipDTO)
        {
            // Don't allow wrong data
            if (id != friendshipDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and friendship.id do not match"));
            }
            var friendship = await _bll.Friendships.FirstOrDefaultAsync(friendshipDTO.Id, User.UserGuidId());
            if (friendship == null)
            {
                _logger.LogError($"EDIT. No such friendship: {friendshipDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Friendship found for id {id}"));
            }
            // Update existing friendship
            await _bll.Friendships.UpdateAsync(_mapper.Map(friendshipDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        // POST: api/Friendships
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new pending Friendship - when request is sent but not yet accepted
        /// </summary>
        /// <param name="friendshipDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.FriendshipDTO))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> PostPendingFriendship(V1DTO.FriendshipDTO friendshipDTO)
        {
            // if (friendshipDTO.AppUser2Id == User.UserGuidId())
            // {
            //     return BadRequest(new V1DTO.MessageDTO($"Could not add friend with id {friendshipDTO.AppUser2Id}"));
            // }
            // Create pending friendship
            var bllEntity = _mapper.Map(friendshipDTO);
            bllEntity.IsConfirmed = false;
            _bll.Friendships.Add(bllEntity);
            await _bll.SaveChangesAsync();

            friendshipDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetFriendship",
                new {id = friendshipDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                friendshipDTO
            );
        }

        // POST: api/Friendships/Accepted
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Friendship - when friend request is accepted
        /// </summary>
        /// <param name="friendshipDTO"></param>
        /// <returns></returns>
        [HttpPost("accepted")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.FriendshipDTO))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> PostAcceptedFriendship(V1DTO.FriendshipDTO friendshipDTO)
        {
            // Create accepted friendship
            var bllEntity = _mapper.Map(friendshipDTO);
            bllEntity.IsConfirmed = true;
            _bll.Friendships.Add(bllEntity);
            await _bll.SaveChangesAsync();

            friendshipDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetFriendship",
                new {id = friendshipDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                friendshipDTO
                );
        }

        // DELETE: api/Friendships/5
        /// <summary>
        ///     Delete friendship - remove pending request or unfriend an existing friend
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FriendshipDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> DeleteFriendship(Guid id)
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
