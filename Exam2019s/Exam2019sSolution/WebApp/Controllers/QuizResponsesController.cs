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
    public class QuizResponsesController : Controller
    {
        private readonly AppDbContext _context;

        public QuizResponsesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuizResponses
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.QuizResponses.Include(q => q.Answer).Include(q => q.AppUser).Include(q => q.Question).Include(q => q.Quiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: QuizResponses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResponse = await _context.QuizResponses
                .Include(q => q.Answer)
                .Include(q => q.AppUser)
                .Include(q => q.Question)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResponse == null)
            {
                return NotFound();
            }

            return View(quizResponse);
        }

        // GET: QuizResponses/Create
        public IActionResult Create()
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Name");
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Name");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name");
            return View();
        }

        // POST: QuizResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,QuestionId,QuizId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] QuizResponse quizResponse)
        {
            if (ModelState.IsValid)
            {
                quizResponse.Id = Guid.NewGuid();
                _context.Add(quizResponse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Name", quizResponse.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", quizResponse.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Name", quizResponse.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name", quizResponse.QuizId);
            return View(quizResponse);
        }

        // GET: QuizResponses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResponse = await _context.QuizResponses.FindAsync(id);
            if (quizResponse == null)
            {
                return NotFound();
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Name", quizResponse.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", quizResponse.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Name", quizResponse.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name", quizResponse.QuizId);
            return View(quizResponse);
        }

        // POST: QuizResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnswerId,QuestionId,QuizId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] QuizResponse quizResponse)
        {
            if (id != quizResponse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizResponseExists(quizResponse.Id))
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
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "Name", quizResponse.AnswerId);
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", quizResponse.AppUserId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Name", quizResponse.QuestionId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name", quizResponse.QuizId);
            return View(quizResponse);
        }

        // GET: QuizResponses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResponse = await _context.QuizResponses
                .Include(q => q.Answer)
                .Include(q => q.AppUser)
                .Include(q => q.Question)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResponse == null)
            {
                return NotFound();
            }

            return View(quizResponse);
        }

        // POST: QuizResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizResponse = await _context.QuizResponses.FindAsync(id);
            _context.QuizResponses.Remove(quizResponse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizResponseExists(Guid id)
        {
            return _context.QuizResponses.Any(e => e.Id == id);
        }
    }
}
