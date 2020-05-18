#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class UserPermissionsController : Controller
    {
        private readonly AppDbContext _context;

        public UserPermissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserPermissions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserPermissions.Include(u => u.AppUser).Include(u => u.Permission);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserPermissions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermission = await _context.UserPermissions
                .Include(u => u.AppUser)
                .Include(u => u.Permission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPermission == null)
            {
                return NotFound();
            }

            return View(userPermission);
        }

        // GET: UserPermissions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "PermissionValue");
            return View();
        }

        // POST: UserPermissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("From,To,Comment,PermissionId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] UserPermission userPermission)
        {
            if (ModelState.IsValid)
            {
                userPermission.Id = Guid.NewGuid();
                _context.Add(userPermission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userPermission.AppUserId);
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "PermissionValue", userPermission.PermissionId);
            return View(userPermission);
        }

        // GET: UserPermissions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermission = await _context.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userPermission.AppUserId);
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "PermissionValue", userPermission.PermissionId);
            return View(userPermission);
        }

        // POST: UserPermissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("From,To,Comment,PermissionId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] UserPermission userPermission)
        {
            if (id != userPermission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPermission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPermissionExists(userPermission.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userPermission.AppUserId);
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "PermissionValue", userPermission.PermissionId);
            return View(userPermission);
        }

        // GET: UserPermissions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermission = await _context.UserPermissions
                .Include(u => u.AppUser)
                .Include(u => u.Permission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPermission == null)
            {
                return NotFound();
            }

            return View(userPermission);
        }

        // POST: UserPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userPermission = await _context.UserPermissions.FindAsync(id);
            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPermissionExists(Guid id)
        {
            return _context.UserPermissions.Any(e => e.Id == id);
        }
    }
}
