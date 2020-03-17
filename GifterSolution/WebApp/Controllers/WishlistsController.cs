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
    public class WishlistsController : Controller
    {
        private readonly AppDbContext _context;

        public WishlistsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Wishlists
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Wishlists.Include(w => w.Gift);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists
                .Include(w => w.Gift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "ActionTypeId");
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,GiftId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                wishlist.Id = Guid.NewGuid();
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "ActionTypeId", wishlist.GiftId);
            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "ActionTypeId", wishlist.GiftId);
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Comment,GiftId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Wishlist wishlist)
        {
            if (id != wishlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.Id))
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
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "ActionTypeId", wishlist.GiftId);
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlists
                .Include(w => w.Gift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id);
            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(Guid id)
        {
            return _context.Wishlists.Any(e => e.Id == id);
        }
    }
}
