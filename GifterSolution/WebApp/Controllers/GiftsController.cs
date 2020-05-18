#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class GiftsController : Controller
    {
        private readonly AppDbContext _context;

        public GiftsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Gifts.Include(g => g.ActionType).Include(g => g.Status).Include(g => g.Wishlist);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gifts
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.Wishlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue");
            ViewData["WishlistId"] = new SelectList(_context.Wishlists, "Id", "Id");
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Image,Url,PartnerUrl,IsPartnered,IsPinned,ActionTypeId,StatusId,WishlistId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                gift.Id = Guid.NewGuid();
                _context.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", gift.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", gift.StatusId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlists, "Id", "Id", gift.WishlistId);
            return View(gift);
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", gift.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", gift.StatusId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlists, "Id", "Id", gift.WishlistId);
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Image,Url,PartnerUrl,IsPartnered,IsPinned,ActionTypeId,StatusId,WishlistId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] Gift gift)
        {
            if (id != gift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftExists(gift.Id))
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
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", gift.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", gift.StatusId);
            ViewData["WishlistId"] = new SelectList(_context.Wishlists, "Id", "Id", gift.WishlistId);
            return View(gift);
        }

        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gifts
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.Wishlist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftExists(Guid id)
        {
            return _context.Gifts.Any(e => e.Id == id);
        }
    }
}
