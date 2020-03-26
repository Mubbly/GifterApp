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
    public class FriendshipsController : Controller
    {
        // TODO: Use uow
        private readonly AppDbContext _context;

        public FriendshipsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Friendships
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Friendships.Include(f => f.AppUser1).Include(f => f.AppUser2);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Friendships/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendships
                .Include(f => f.AppUser1)
                .Include(f => f.AppUser2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friendship == null)
            {
                return NotFound();
            }

            return View(friendship);
        }

        // GET: Friendships/Create
        public IActionResult Create()
        {
            ViewData["AppUser1Id"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["AppUser2Id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Friendships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsConfirmed,Comment,AppUser1Id,AppUser2Id,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                friendship.Id = Guid.NewGuid();
                _context.Add(friendship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUser1Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser1Id);
            ViewData["AppUser2Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser2Id);
            return View(friendship);
        }

        // GET: Friendships/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }
            ViewData["AppUser1Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser1Id);
            ViewData["AppUser2Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser2Id);
            return View(friendship);
        }

        // POST: Friendships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IsConfirmed,Comment,AppUser1Id,AppUser2Id,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Friendship friendship)
        {
            if (id != friendship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friendship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendshipExists(friendship.Id))
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
            ViewData["AppUser1Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser1Id);
            ViewData["AppUser2Id"] = new SelectList(_context.Users, "Id", "Id", friendship.AppUser2Id);
            return View(friendship);
        }

        // GET: Friendships/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendship = await _context.Friendships
                .Include(f => f.AppUser1)
                .Include(f => f.AppUser2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friendship == null)
            {
                return NotFound();
            }

            return View(friendship);
        }

        // POST: Friendships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendshipExists(Guid id)
        {
            return _context.Friendships.Any(e => e.Id == id);
        }
    }
}
