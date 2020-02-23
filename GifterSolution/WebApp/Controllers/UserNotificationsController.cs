using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class UserNotificationsController : Controller
    {
        private readonly AppDbContext _context;

        public UserNotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserNotifications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserNotifications.Include(u => u.AppUser).Include(u => u.Notification);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserNotifications/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications
                .Include(u => u.AppUser)
                .Include(u => u.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // GET: UserNotifications/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id");
            return View();
        }

        // POST: UserNotifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastNotified,RenotifyAt,IsActive,IsDisabled,Comment,AppUserId,NotificationId,CreatedBy,CreatedAt,EditedBy,EditedAt,DeletedBy,DeletedAt,Id")] UserNotification userNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.AppUserId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id", userNotification.NotificationId);
            return View(userNotification);
        }

        // GET: UserNotifications/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications.FindAsync(id);
            if (userNotification == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.AppUserId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id", userNotification.NotificationId);
            return View(userNotification);
        }

        // POST: UserNotifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LastNotified,RenotifyAt,IsActive,IsDisabled,Comment,AppUserId,NotificationId,CreatedBy,CreatedAt,EditedBy,EditedAt,DeletedBy,DeletedAt,Id")] UserNotification userNotification)
        {
            if (id != userNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserNotificationExists(userNotification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userNotification.AppUserId);
            ViewData["NotificationId"] = new SelectList(_context.Notifications, "Id", "Id", userNotification.NotificationId);
            return View(userNotification);
        }

        // GET: UserNotifications/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userNotification = await _context.UserNotifications
                .Include(u => u.AppUser)
                .Include(u => u.Notification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userNotification == null)
            {
                return NotFound();
            }

            return View(userNotification);
        }

        // POST: UserNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userNotification = await _context.UserNotifications.FindAsync(id);
            _context.UserNotifications.Remove(userNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserNotificationExists(string id)
        {
            return _context.UserNotifications.Any(e => e.Id == id);
        }
    }
}
