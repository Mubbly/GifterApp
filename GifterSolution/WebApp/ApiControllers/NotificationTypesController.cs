using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NotificationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationTypeDTO>>> GetNotificationTypes()
        {
            return await _context.NotificationTypes
                .Select(nt => new NotificationTypeDTO() 
                {
                    Id = nt.Id,
                    Comment = nt.Comment,
                    NotificationsCount = nt.Notifications.Count,
                    NotificationTypeValue = nt.NotificationTypeValue
                }).ToListAsync();
        }

        // GET: api/NotificationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationTypeDTO>> GetNotificationType(Guid id)
        {
            var notificationType = await _context.NotificationTypes
                .Select(nt => new NotificationTypeDTO() 
                {
                    Id = nt.Id,
                    Comment = nt.Comment,
                    NotificationsCount = nt.Notifications.Count,
                    NotificationTypeValue = nt.NotificationTypeValue
                }).SingleOrDefaultAsync();

            if (notificationType == null)
            {
                return NotFound();
            }

            return notificationType;
        }

        // PUT: api/NotificationTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationType(Guid id, NotificationType notificationType)
        {
            if (id != notificationType.Id)
            {
                return BadRequest();
            }

            _context.Entry(notificationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationTypeExists(id))
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

        // POST: api/NotificationTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<NotificationType>> PostNotificationType(NotificationType notificationType)
        {
            _context.NotificationTypes.Add(notificationType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationType", new { id = notificationType.Id }, notificationType);
        }

        // DELETE: api/NotificationTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NotificationType>> DeleteNotificationType(Guid id)
        {
            var notificationType = await _context.NotificationTypes.FindAsync(id);
            if (notificationType == null)
            {
                return NotFound();
            }

            _context.NotificationTypes.Remove(notificationType);
            await _context.SaveChangesAsync();

            return notificationType;
        }

        private bool NotificationTypeExists(Guid id)
        {
            return _context.NotificationTypes.Any(e => e.Id == id);
        }
    }
}