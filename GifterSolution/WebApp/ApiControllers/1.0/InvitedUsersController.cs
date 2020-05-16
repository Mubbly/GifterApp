using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
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

        // GET: api/InvitedUsers
        /// <summary>
        ///     Get all InvitedUsers
        /// </summary>
        /// <returns>List of InvitedUsers</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.InvitedUserDTO>>> GetInvitedUsers()
        {
            return Ok((await _bll.InvitedUsers.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/InvitedUsers/5
        /// <summary>
        ///     Get a single InvitedUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns>InvitedUser object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> GetInvitedUser(Guid id)
        {
            var actionType = await _bll.InvitedUsers.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/InvitedUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update InvitedUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutInvitedUser(Guid id, V1DTO.InvitedUserDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.InvitedUsers.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No InvitedUser found for id {id}"));
            }
            // Update existing actionType
            // actionType.InvitedUserValue = actionTypeEditDTO.InvitedUserValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.InvitedUsers.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.InvitedUsers.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. InvitedUser does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/InvitedUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new InvitedUser
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.InvitedUserDTO))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> PostInvitedUser(V1DTO.InvitedUserDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.InvitedUsers.Add(bllEntity);
            
            // var actionType = new InvitedUser
            // {
            //     InvitedUserValue = actionTypeCreateDTO.InvitedUserValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetInvitedUser",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.InvitedUserDTO>> DeleteInvitedUser(Guid id)
        {
            var actionType = await _bll.InvitedUsers.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }
            await _bll.InvitedUsers.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }

        // // GET: api/InvitedUsers
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.InvitedUserDTO>>> GetInvitedUsers()
        // {
        //     return await _context.InvitedUsers
        //         .Select(iu => new V1DTO.InvitedUserDTO() 
        //         {
        //             Id = iu.Id,
        //             Email = iu.Email,
        //             Message = iu.Email,
        //             DateInvited = iu.DateInvited,
        //             HasJoined = iu.HasJoined,
        //             InvitorUserId = iu.InvitorUserId,
        //             PhoneNumber = iu.PhoneNumber
        //         }).ToListAsync();
        // }
        //
        // // GET: api/InvitedUsers/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.InvitedUserDTO>> GetInvitedUser(Guid id)
        // {
        //     var invitedUser = await _context.InvitedUsers
        //         .Select(iu => new V1DTO.InvitedUserDTO() 
        //         {
        //             Id = iu.Id,
        //             Email = iu.Email,
        //             Message = iu.Email,
        //             DateInvited = iu.DateInvited,
        //             HasJoined = iu.HasJoined,
        //             InvitorUserId = iu.InvitorUserId,
        //             PhoneNumber = iu.PhoneNumber
        //         }).SingleOrDefaultAsync();
        //
        //     if (invitedUser == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return invitedUser;
        // }
        //
        // // PUT: api/InvitedUsers/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutInvitedUser(Guid id, InvitedUser invitedUser)
        // {
        //     if (id != invitedUser.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(invitedUser).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!InvitedUserExists(id))
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
        // // POST: api/InvitedUsers
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<InvitedUser>> PostInvitedUser(InvitedUser invitedUser)
        // {
        //     _context.InvitedUsers.Add(invitedUser);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetInvitedUser", new { id = invitedUser.Id }, invitedUser);
        // }
        //
        // // DELETE: api/InvitedUsers/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<InvitedUser>> DeleteInvitedUser(Guid id)
        // {
        //     var invitedUser = await _context.InvitedUsers.FindAsync(id);
        //     if (invitedUser == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.InvitedUsers.Remove(invitedUser);
        //     await _context.SaveChangesAsync();
        //
        //     return invitedUser;
        // }
        //
        // private bool InvitedUserExists(Guid id)
        // {
        //     return _context.InvitedUsers.Any(e => e.Id == id);
        // }
    }
}
