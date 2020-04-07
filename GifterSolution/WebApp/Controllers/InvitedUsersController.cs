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
    public class InvitedUsersController : Controller
    {
        private readonly AppDbContext _context;

        public InvitedUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InvitedUsers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.InvitedUsers.Include(i => i.InvitorUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: InvitedUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitedUser = await _context.InvitedUsers
                .Include(i => i.InvitorUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitedUser == null)
            {
                return NotFound();
            }

            return View(invitedUser);
        }

        // GET: InvitedUsers/Create
        public IActionResult Create()
        {
            ViewData["InvitorUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: InvitedUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,PhoneNumber,Message,DateInvited,HasJoined,InvitorUserId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] InvitedUser invitedUser)
        {
            if (ModelState.IsValid)
            {
                invitedUser.Id = Guid.NewGuid();
                _context.Add(invitedUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvitorUserId"] = new SelectList(_context.Users, "Id", "FirstName", invitedUser.InvitorUserId);
            return View(invitedUser);
        }

        // GET: InvitedUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitedUser = await _context.InvitedUsers.FindAsync(id);
            if (invitedUser == null)
            {
                return NotFound();
            }
            ViewData["InvitorUserId"] = new SelectList(_context.Users, "Id", "FirstName", invitedUser.InvitorUserId);
            return View(invitedUser);
        }

        // POST: InvitedUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Email,PhoneNumber,Message,DateInvited,HasJoined,InvitorUserId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] InvitedUser invitedUser)
        {
            if (id != invitedUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invitedUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvitedUserExists(invitedUser.Id))
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
            ViewData["InvitorUserId"] = new SelectList(_context.Users, "Id", "FirstName", invitedUser.InvitorUserId);
            return View(invitedUser);
        }

        // GET: InvitedUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invitedUser = await _context.InvitedUsers
                .Include(i => i.InvitorUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitedUser == null)
            {
                return NotFound();
            }

            return View(invitedUser);
        }

        // POST: InvitedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invitedUser = await _context.InvitedUsers.FindAsync(id);
            _context.InvitedUsers.Remove(invitedUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvitedUserExists(Guid id)
        {
            return _context.InvitedUsers.Any(e => e.Id == id);
        }
    }
}
