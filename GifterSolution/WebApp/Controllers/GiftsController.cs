using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles="User")]
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
            var appDbContext = _context.Gifts
                .Include(g => g.ActionType)
                .Include(g => g.AppUser)
                .Include(g => g.Status)
                .Where(g => g.AppUserId == User.UserGuidId());
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
                .Include(g => g.AppUser)
                .Include(g => g.Status)
                .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == User.UserGuidId());
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
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gift gift)
        {
            gift.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                //gift.Id = Guid.NewGuid();
                _context.Add(gift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", gift.ActionTypeId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", gift.AppUserId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", gift.StatusId);
            return View(gift);
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _context.Gifts
                .FirstOrDefaultAsync(g => g.Id == id && g.AppUserId == User.UserGuidId());
            if (gift == null)
            {
                return NotFound();
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", gift.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", gift.StatusId);
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Gift gift)
        {
            gift.AppUserId = User.UserGuidId();
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
                .Include(g => g.AppUser)
                .Include(g => g.Status)
                .FirstOrDefaultAsync(m => m.Id == id  && m.AppUserId == User.UserGuidId());
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
            var gift = await _context.Gifts
                .FirstOrDefaultAsync(m => m.Id == id  && m.AppUserId == User.UserGuidId());
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftExists(Guid id)
        {
            return _context.Gifts.Any(e => e.Id == id && e.AppUserId == User.UserGuidId());
        }
    }
}
