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
            // TODO: Move to BLL
            var invitedUsers = await _bll.InvitedUsers.GetAllAsync();
            var personalUsersBll = invitedUsers
                .Where(u => u.InvitorUserId == User.UserGuidId())
                .ToList();
            if (!personalUsersBll.Any())
            {
                return NotFound(new V1DTO.MessageDTO($"No invited users found for user {User.UserGuidId()}"));
            }
            var personalInvitedUsers = personalUsersBll.Select(e => _mapper.Map(e));
            return Ok(personalInvitedUsers);
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
            var invitedUser = await _bll.InvitedUsers.FirstOrDefaultAsync(id);
            if (invitedUser == null)
            {
                return NotFound(new V1DTO.MessageDTO($"InvitedUser with id {id} not found"));
            }
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
            // invitedUser.InvitedUserValue = invitedUserEditDTO.InvitedUserValue;
            // invitedUser.Comment = invitedUserEditDTO.Comment;
            await _bll.InvitedUsers.UpdateAsync(_mapper.Map(invitedUserDTO), User.UserId());

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
            
            _bll.InvitedUsers.Add(bllEntity);
            
            // var invitedUser = new InvitedUser
            // {
            //     InvitedUserValue = invitedUserCreateDTO.InvitedUserValue,
            //     Comment = invitedUserCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            invitedUserDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetInvitedUser",
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
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
