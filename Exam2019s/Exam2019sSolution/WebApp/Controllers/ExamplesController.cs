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
    public class ExamplesController : Controller
    {
        private readonly AppDbContext _context;

        public ExamplesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Example
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Examples.Include(e => e.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Example/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var example = await _context.Examples
                .Include(e => e.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (example == null)
            {
                return NotFound();
            }

            return View(example);
        }

        // GET: Example/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: Example/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Example example)
        {
            if (ModelState.IsValid)
            {
                example.Id = Guid.NewGuid();
                _context.Add(example);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", example.AppUserId);
            return View(example);
        }

        // GET: Example/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var example = await _context.Examples.FindAsync(id);
            if (example == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", example.AppUserId);
            return View(example);
        }

        // POST: Example/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Example example)
        {
            if (id != example.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(example);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExampleExists(example.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", example.AppUserId);
            return View(example);
        }

        // GET: Example/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var example = await _context.Examples
                .Include(e => e.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (example == null)
            {
                return NotFound();
            }

            return View(example);
        }

        // POST: Example/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var example = await _context.Examples.FindAsync(id);
            _context.Examples.Remove(example);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExampleExists(Guid id)
        {
            return _context.Examples.Any(e => e.Id == id);
        }
    }
}
