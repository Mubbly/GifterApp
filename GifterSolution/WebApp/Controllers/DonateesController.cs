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
    public class DonateesController : Controller
    {
        private readonly AppDbContext _context;

        public DonateesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Donatees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Donatees.Include(d => d.ActionType).Include(d => d.Status);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Donatees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatee = await _context.Donatees
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donatee == null)
            {
                return NotFound();
            }

            return View(donatee);
        }

        // GET: Donatees/Create
        public IActionResult Create()
        {
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue");
            return View();
        }

        // POST: Donatees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Gender,Age,Bio,GiftName,GiftDescription,GiftImage,GiftUrl,GiftReservedFrom,ActiveFrom,ActiveTo,IsActive,ActionTypeId,StatusId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] Donatee donatee)
        {
            if (ModelState.IsValid)
            {
                donatee.Id = Guid.NewGuid();
                _context.Add(donatee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", donatee.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", donatee.StatusId);
            return View(donatee);
        }

        // GET: Donatees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatee = await _context.Donatees.FindAsync(id);
            if (donatee == null)
            {
                return NotFound();
            }
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", donatee.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", donatee.StatusId);
            return View(donatee);
        }

        // POST: Donatees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,Gender,Age,Bio,GiftName,GiftDescription,GiftImage,GiftUrl,GiftReservedFrom,ActiveFrom,ActiveTo,IsActive,ActionTypeId,StatusId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] Donatee donatee)
        {
            if (id != donatee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donatee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonateeExists(donatee.Id))
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
            ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", donatee.ActionTypeId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", donatee.StatusId);
            return View(donatee);
        }

        // GET: Donatees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donatee = await _context.Donatees
                .Include(d => d.ActionType)
                .Include(d => d.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donatee == null)
            {
                return NotFound();
            }

            return View(donatee);
        }

        // POST: Donatees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var donatee = await _context.Donatees.FindAsync(id);
            _context.Donatees.Remove(donatee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonateeExists(Guid id)
        {
            return _context.Donatees.Any(e => e.Id == id);
        }
    }
}
