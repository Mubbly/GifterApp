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
    public class QuizTypesController : Controller
    {
        private readonly AppDbContext _context;

        public QuizTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuizTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuizTypes.ToListAsync());
        }

        // GET: QuizTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizType = await _context.QuizTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizType == null)
            {
                return NotFound();
            }

            return View(quizType);
        }

        // GET: QuizTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuizTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] QuizType quizType)
        {
            if (ModelState.IsValid)
            {
                quizType.Id = Guid.NewGuid();
                _context.Add(quizType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizType);
        }

        // GET: QuizTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizType = await _context.QuizTypes.FindAsync(id);
            if (quizType == null)
            {
                return NotFound();
            }
            return View(quizType);
        }

        // POST: QuizTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] QuizType quizType)
        {
            if (id != quizType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizTypeExists(quizType.Id))
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
            return View(quizType);
        }

        // GET: QuizTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizType = await _context.QuizTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizType == null)
            {
                return NotFound();
            }

            return View(quizType);
        }

        // POST: QuizTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizType = await _context.QuizTypes.FindAsync(id);
            _context.QuizTypes.Remove(quizType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizTypeExists(Guid id)
        {
            return _context.QuizTypes.Any(e => e.Id == id);
        }
    }
}
