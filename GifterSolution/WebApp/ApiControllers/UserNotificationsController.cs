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
    public class UserNotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserNotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNotification>>> GetUserNotifications()
        {
            return await _context.UserNotifications
                .Where(un => un.AppUserId == User.UserGuidId())
                .ToListAsync();
        }

        // GET: api/UserNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserNotification>> GetUserNotification(Guid id)
        {
            var userNotification = await _context.UserNotifications
                .FirstOrDefaultAsync(un => un.Id == id && un.AppUserId == User.UserGuidId());

            if (userNotification == null)
            {
                return NotFound();
            }

            return userNotification;
        }

        // PUT: api/UserNotifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserNotification(Guid id, UserNotification userNotification)
        {
            if (id != userNotification.Id)
            {
                return BadRequest();
            }

            _context.Entry(userNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserNotificationExists(id))
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

        // POST: api/UserNotifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserNotification>> PostUserNotification(UserNotification userNotification)
        {
            _context.UserNotifications.Add(userNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserNotification", new { id = userNotification.Id }, userNotification);
        }

        // DELETE: api/UserNotifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserNotification>> DeleteUserNotification(Guid id)
        {
            var userNotification = await _context.UserNotifications.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }

            _context.UserNotifications.Remove(userNotification);
            await _context.SaveChangesAsync();

            return userNotification;
        }

        private bool UserNotificationExists(Guid id)
        {
            return _context.UserNotifications.Any(e => e.Id == id);
        }
    }
}
