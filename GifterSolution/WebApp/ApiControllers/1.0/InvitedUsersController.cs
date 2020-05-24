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
    public class InvitedUsersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<InvitedUsersController> _logger;
        private readonly InvitedUserMapper _mapper = new InvitedUserMapper();

        public InvitedUsersController(IAppBLL appBLL, ILogger<InvitedUsersController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }

        // GET: api/InvitedUsers/personal
        /// <summary>
        ///     Get all personal InvitedUsers
        /// </summary>
        /// <returns>List of InvitedUsers</returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.InvitedUserDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.InvitedUserDTO>>> GetPersonalInvitedUsers()
        {
            var personalInvitedUsers = await _bll.InvitedUsers.GetAllPersonalAsync(User.UserGuidId());
            return Ok(personalInvitedUsers.Select(e => _mapper.Map(e)));
        }

        // GET: api/InvitedUsers/personal/5
        /// <summary>
        ///     Get a single personal InvitedUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InvitedUser object</returns>
        [HttpGet("personal/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.InvitedUserDTO>))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> GetPersonalInvitedUser(Guid id)
        {
            var invitedUser = await _bll.InvitedUsers.FirstOrDefaultAsync(id);
            if (invitedUser == null)
            {
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }
            // Allow only personally invited users - TODO BLL
            if (invitedUser.InvitorUserId != User.UserGuidId())
            {
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }
            return Ok(_mapper.Map(invitedUser));
        }

        // PUT: api/InvitedUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update InvitedUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invitedUserDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutInvitedUser(Guid id, V1DTO.InvitedUserDTO invitedUserDTO)
        {
            // Don't allow wrong data
            if (id != invitedUserDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and invitedUser.id do not match"));
            }
            var invitedUser = await _bll.InvitedUsers.FirstOrDefaultAsync(invitedUserDTO.Id, User.UserGuidId());
            if (invitedUser == null)
            {
                _logger.LogError($"EDIT. No such invitedUser: {invitedUserDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No InvitedUser found for id {id}"));
            }
            // Update existing invitedUser
            await _bll.InvitedUsers.UpdateAsync(_mapper.Map(invitedUserDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/InvitedUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new InvitedUser
        /// </summary>
        /// <param name="invitedUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.InvitedUserDTO))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> PostInvitedUser(V1DTO.InvitedUserDTO invitedUserDTO)
        {
            // Create invitedUser
            var bllEntity = _mapper.Map(invitedUserDTO);
            bllEntity.DateInvited = DateTime.Now;
            bllEntity.HasJoined = false;
            bllEntity.InvitorUserId = User.UserGuidId();
            // Add to db
            _bll.InvitedUsers.Add(bllEntity);
            await _bll.SaveChangesAsync();

            invitedUserDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetPersonalInvitedUser",
                new {id = invitedUserDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                invitedUserDTO
                );
        }

        // DELETE: api/InvitedUsers/5
        /// <summary>
        ///     Delete InvitedUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.InvitedUserDTO>))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> DeleteInvitedUser(Guid id)
        {
            var invitedUser = await _bll.InvitedUsers.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invitedUser == null)
            {
                _logger.LogError($"DELETE. No such invitedUser: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }
            await _bll.InvitedUsers.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(invitedUser);
        }
    }
}
