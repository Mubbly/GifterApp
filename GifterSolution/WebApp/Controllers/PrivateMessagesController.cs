using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PrivateMessagesController : Controller
    {
        // TODO: Use uow
        private readonly AppDbContext _context;

        public PrivateMessagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PrivateMessages
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PrivateMessages.Include(p => p.UserReceiver).Include(p => p.UserSender);
            return View(await appDbContext.ToListAsync());
        }

        // GET: PrivateMessages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privateMessage = await _context.PrivateMessages
                .Include(p => p.UserReceiver)
                .Include(p => p.UserSender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privateMessage == null)
            {
                return NotFound();
            }

            return View(privateMessage);
        }

        // GET: PrivateMessages/Create
        public IActionResult Create()
        {
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserSenderId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PrivateMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,SentAt,IsSeen,UserSenderId,UserReceiverId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] PrivateMessage privateMessage)
        {
            if (ModelState.IsValid)
            {
                privateMessage.Id = Guid.NewGuid();
                _context.Add(privateMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserReceiverId);
            ViewData["UserSenderId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserSenderId);
            return View(privateMessage);
        }

        // GET: PrivateMessages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privateMessage = await _context.PrivateMessages.FindAsync(id);
            if (privateMessage == null)
            {
                return NotFound();
            }
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserReceiverId);
            ViewData["UserSenderId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserSenderId);
            return View(privateMessage);
        }

        // POST: PrivateMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Message,SentAt,IsSeen,UserSenderId,UserReceiverId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] PrivateMessage privateMessage)
        {
            if (id != privateMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privateMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivateMessageExists(privateMessage.Id))
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
            ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserReceiverId);
            ViewData["UserSenderId"] = new SelectList(_context.Users, "Id", "Id", privateMessage.UserSenderId);
            return View(privateMessage);
        }

        // GET: PrivateMessages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var privateMessage = await _context.PrivateMessages
                .Include(p => p.UserReceiver)
                .Include(p => p.UserSender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privateMessage == null)
            {
                return NotFound();
            }

            return View(privateMessage);
        }

        // POST: PrivateMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var privateMessage = await _context.PrivateMessages.FindAsync(id);
            _context.PrivateMessages.Remove(privateMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrivateMessageExists(Guid id)
        {
            return _context.PrivateMessages.Any(e => e.Id == id);
        }
    }
}
