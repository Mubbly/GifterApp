#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class NotificationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public NotificationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NotificationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.NotificationTypes.ToListAsync());
        }

        // GET: NotificationTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _context.NotificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificationType == null)
            {
                return NotFound();
            }

            return View(notificationType);
        }

        // GET: NotificationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationTypeValue,Comment,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] NotificationType notificationType)
        {
            if (ModelState.IsValid)
            {
                notificationType.Id = Guid.NewGuid();
                _context.Add(notificationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notificationType);
        }

        // GET: NotificationTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _context.NotificationTypes.FindAsync(id);
            if (notificationType == null)
            {
                return NotFound();
            }
            return View(notificationType);
        }

        // POST: NotificationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NotificationTypeValue,Comment,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] NotificationType notificationType)
        {
            if (id != notificationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notificationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotificationTypeExists(notificationType.Id))
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
            return View(notificationType);
        }

        // GET: NotificationTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _context.NotificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notificationType == null)
            {
                return NotFound();
            }

            return View(notificationType);
        }

        // POST: NotificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notificationType = await _context.NotificationTypes.FindAsync(id);
            _context.NotificationTypes.Remove(notificationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationTypeExists(Guid id)
        {
            return _context.NotificationTypes.Any(e => e.Id == id);
        }
    }
}
