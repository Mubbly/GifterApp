using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InvitedUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvitedUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/InvitedUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvitedUser>>> GetInvitedUsers()
        {
            return await _context.InvitedUsers
                .Where(iu => iu.InvitorUserId == User.UserGuidId())
                .ToListAsync();
        }

        // GET: api/InvitedUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvitedUser>> GetInvitedUser(Guid id)
        {
            var invitedUser = await _context.InvitedUsers
                .FirstOrDefaultAsync(iu => iu.Id == id && iu.InvitorUserId == User.UserGuidId());

            if (invitedUser == null)
            {
                return NotFound();
            }

            return invitedUser;
        }

        // PUT: api/InvitedUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvitedUser(Guid id, InvitedUser invitedUser)
        {
            if (id != invitedUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(invitedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitedUserExists(id))
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
        public async Task<ActionResult<InvitedUser>> PostInvitedUser(InvitedUser invitedUser)
        {
            _context.InvitedUsers.Add(invitedUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvitedUser", new { id = invitedUser.Id }, invitedUser);
        }

        // DELETE: api/InvitedUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvitedUser>> DeleteInvitedUser(Guid id)
        {
            var invitedUser = await _context.InvitedUsers.FindAsync(id);
            if (invitedUser == null)
            {
                return NotFound();
            }

            _context.InvitedUsers.Remove(invitedUser);
            await _context.SaveChangesAsync();

            return invitedUser;
        }

        private bool InvitedUserExists(Guid id)
        {
            return _context.InvitedUsers.Any(e => e.Id == id);
        }
    }
}
