using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvitedUsersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<InvitedUsersController> _logger;
        
        public InvitedUsersController(IAppUnitOfWork uow, ILogger<InvitedUsersController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET: api/InvitedUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvitedUserDTO>>> GetInvitedUsers()
        {
            // Only allow users to see their own invitations
            return Ok(await _uow.InvitedUsers.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/InvitedUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvitedUserDTO>> GetInvitedUser(Guid id)
        {
            // Only allow users to see their own invitations
            var invitedUserDTO = await _uow.InvitedUsers.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            if (invitedUserDTO == null)
            {
                return NotFound();
            }
            return Ok(invitedUserDTO);
        }

        // PUT: api/InvitedUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvitedUser(Guid id, InvitedUserEditDTO invitedUserEditDTO)
        {
            if (id != invitedUserEditDTO.Id)
            {
                return BadRequest();
            }
            
            // Only allow users to edit invitations they created
            var invitedUser = await _uow.InvitedUsers.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invitedUser == null)
            {
                return BadRequest();
            }

            // Update existing invitedUser
            invitedUser.Email = invitedUserEditDTO.Email;
            invitedUser.PhoneNumber = invitedUserEditDTO.PhoneNumber;
            invitedUser.Message = invitedUserEditDTO.Message;

            _uow.InvitedUsers.Update(invitedUser);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.InvitedUsers.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InvitedUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InvitedUserCreateDTO>> PostInvitedUser(InvitedUserCreateDTO invitedUserCreateDTO)
        {
            // Allow all users create invitations
            var invitedUser = new InvitedUser
            {
                Email = invitedUserCreateDTO.Email,
                PhoneNumber = invitedUserCreateDTO.PhoneNumber,
                Message = invitedUserCreateDTO.Message,
                DateInvited = DateTime.Now,
                HasJoined = false,
                InvitorUserId = User.UserGuidId()
            };
            
            _uow.InvitedUsers.Add(invitedUser);
            await _uow.SaveChangesAsync();

            // Send response back to user
            invitedUserCreateDTO.Id = invitedUser.Id;
            return CreatedAtAction("GetInvitedUser", new { id = invitedUser.Id }, invitedUserCreateDTO);
        }

        // DELETE: api/InvitedUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvitedUser>> DeleteInvitedUser(Guid id)
        {
            // Only allow users to delete their own invitations
            var invitedUser = await _uow.InvitedUsers.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invitedUser == null)
            {
                return NotFound();
            }

            _uow.InvitedUsers.Remove(invitedUser);
            await _uow.SaveChangesAsync();

            return Ok(invitedUser);
        }
    }
}
