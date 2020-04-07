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
    public class ActionTypesController : Controller
    {
        private readonly AppDbContext _context;

        public ActionTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ActionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActionTypes.ToListAsync());
        }

        // GET: ActionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _context.ActionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actionType == null)
            {
                return NotFound();
            }

            return View(actionType);
        }

        // GET: ActionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActionTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] ActionType actionType)
        {
            if (ModelState.IsValid)
            {
                actionType.Id = Guid.NewGuid();
                _context.Add(actionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actionType);
        }

        // GET: ActionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _context.ActionTypes.FindAsync(id);
            if (actionType == null)
            {
                return NotFound();
            }
            return View(actionType);
        }

        // POST: ActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ActionTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] ActionType actionType)
        {
            if (id != actionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionTypeExists(actionType.Id))
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
            return View(actionType);
        }

        // GET: ActionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _context.ActionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actionType == null)
            {
                return NotFound();
            }

            return View(actionType);
        }

        // POST: ActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var actionType = await _context.ActionTypes.FindAsync(id);
            _context.ActionTypes.Remove(actionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionTypeExists(Guid id)
        {
            return _context.ActionTypes.Any(e => e.Id == id);
        }
    }
}
