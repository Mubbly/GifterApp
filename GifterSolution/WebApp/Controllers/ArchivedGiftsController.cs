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
    public class ArchivedGiftsController : Controller
    {
        // TODO: Use uow
        private readonly AppDbContext _context;

        public ArchivedGiftsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ArchivedGifts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ArchivedGifts.Include(a => a.ActionType).Include(a => a.Gift).Include(a => a.Status).Include(a => a.UserGiver).Include(a => a.UserReceiver);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ArchivedGifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivedGift = await _context.ArchivedGifts
                .Include(a => a.ActionType)
                .Include(a => a.Gift)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivedGift == null)
            {
                return NotFound();
            }

            return View(archivedGift);
        }

        // GET: ArchivedGifts/Create
        public IActionResult Create()
        {
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id");
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");
            ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ArchivedGifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateArchived,IsConfirmed,Comment,ActionTypeId,GiftId,StatusId,UserGiverId,UserReceiverId,CreatedBy,CreatedAt,EditedBy,EditedAt,DeletedBy,DeletedAt,Id")] ArchivedGift archivedGift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(archivedGift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id", archivedGift.ActionTypeId);
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Id", archivedGift.GiftId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", archivedGift.StatusId);
            ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserGiverId);
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserReceiverId);
            return View(archivedGift);
        }

        // GET: ArchivedGifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivedGift = await _context.ArchivedGifts.FindAsync(id);
            if (archivedGift == null)
            {
                return NotFound();
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id", archivedGift.ActionTypeId);
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Id", archivedGift.GiftId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", archivedGift.StatusId);
            ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserGiverId);
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserReceiverId);
            return View(archivedGift);
        }

        // POST: ArchivedGifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DateArchived,IsConfirmed,Comment,ActionTypeId,GiftId,StatusId,UserGiverId,UserReceiverId,CreatedBy,CreatedAt,EditedBy,EditedAt,DeletedBy,DeletedAt,Id")] ArchivedGift archivedGift)
        {
            if (id != archivedGift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archivedGift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArchivedGiftExists(archivedGift.Id))
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
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "Id", archivedGift.ActionTypeId);
            ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Id", archivedGift.GiftId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", archivedGift.StatusId);
            ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserGiverId);
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", archivedGift.UserReceiverId);
            return View(archivedGift);
        }

        // GET: ArchivedGifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivedGift = await _context.ArchivedGifts
                .Include(a => a.ActionType)
                .Include(a => a.Gift)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivedGift == null)
            {
                return NotFound();
            }

            return View(archivedGift);
        }

        // POST: ArchivedGifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var archivedGift = await _context.ArchivedGifts.FindAsync(id);
            _context.ArchivedGifts.Remove(archivedGift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivedGiftExists(Guid id)
        {
            return _context.ArchivedGifts.Any(e => e.Id == id);
        }
    }
}
