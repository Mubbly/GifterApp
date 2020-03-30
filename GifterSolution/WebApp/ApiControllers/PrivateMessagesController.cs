using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateMessagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrivateMessagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrivateMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrivateMessageDTO>>> GetPrivateMessages()
        {
            return await _context.PrivateMessages
                .Select(pm => new PrivateMessageDTO() 
                {
                    Id = pm.Id,
                    Message = pm.Message,
                    IsSeen = pm.IsSeen,
                    SentAt = pm.SentAt,
                    UserReceiverId = pm.UserReceiverId,
                    UserSenderId = pm.UserSenderId
                }).ToListAsync();
        }

        // GET: api/PrivateMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrivateMessageDTO>> GetPrivateMessage(Guid id)
        {
            var privateMessage = await _context.PrivateMessages
                .Select(pm => new PrivateMessageDTO() 
                {
                    Id = pm.Id,
                    Message = pm.Message,
                    IsSeen = pm.IsSeen,
                    SentAt = pm.SentAt,
                    UserReceiverId = pm.UserReceiverId,
                    UserSenderId = pm.UserSenderId
                }).SingleOrDefaultAsync();

            if (privateMessage == null)
            {
                return NotFound();
            }

            return privateMessage;
        }

        // PUT: api/PrivateMessages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrivateMessage(Guid id, PrivateMessage privateMessage)
        {
            if (id != privateMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(privateMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrivateMessageExists(id))
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

        // POST: api/PrivateMessages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PrivateMessage>> PostPrivateMessage(PrivateMessage privateMessage)
        {
            _context.PrivateMessages.Add(privateMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrivateMessage", new { id = privateMessage.Id }, privateMessage);
        }

        // DELETE: api/PrivateMessages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PrivateMessage>> DeletePrivateMessage(Guid id)
        {
            var privateMessage = await _context.PrivateMessages.FindAsync(id);
            if (privateMessage == null)
            {
                return NotFound();
            }

            _context.PrivateMessages.Remove(privateMessage);
            await _context.SaveChangesAsync();

            return privateMessage;
        }

        private bool PrivateMessageExists(Guid id)
        {
            return _context.PrivateMessages.Any(e => e.Id == id);
        }
    }
}
