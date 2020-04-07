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
    public class CampaignDonateesController : Controller
    {
        // TODO: Use uow
        private readonly AppDbContext _context;

        public CampaignDonateesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CampaignDonatees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CampaignDonatees.Include(c => c.Campaign).Include(c => c.Donatee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CampaignDonatees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDonatee = await _context.CampaignDonatees
                .Include(c => c.Campaign)
                .Include(c => c.Donatee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignDonatee == null)
            {
                return NotFound();
            }

            return View(campaignDonatee);
        }

        // GET: CampaignDonatees/Create
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name");
            ViewData["DonateeId"] = new SelectList(_context.Donatees, "Id", "FirstName");
            return View();
        }

        // POST: CampaignDonatees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsActive,Comment,CampaignId,DonateeId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] CampaignDonatee campaignDonatee)
        {
            if (ModelState.IsValid)
            {
                campaignDonatee.Id = Guid.NewGuid();
                _context.Add(campaignDonatee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", campaignDonatee.CampaignId);
            ViewData["DonateeId"] = new SelectList(_context.Donatees, "Id", "FirstName", campaignDonatee.DonateeId);
            return View(campaignDonatee);
        }

        // GET: CampaignDonatees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDonatee = await _context.CampaignDonatees.FindAsync(id);
            if (campaignDonatee == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", campaignDonatee.CampaignId);
            ViewData["DonateeId"] = new SelectList(_context.Donatees, "Id", "FirstName", campaignDonatee.DonateeId);
            return View(campaignDonatee);
        }

        // POST: CampaignDonatees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IsActive,Comment,CampaignId,DonateeId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] CampaignDonatee campaignDonatee)
        {
            if (id != campaignDonatee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaignDonatee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignDonateeExists(campaignDonatee.Id))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", campaignDonatee.CampaignId);
            ViewData["DonateeId"] = new SelectList(_context.Donatees, "Id", "FirstName", campaignDonatee.DonateeId);
            return View(campaignDonatee);
        }

        // GET: CampaignDonatees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaignDonatee = await _context.CampaignDonatees
                .Include(c => c.Campaign)
                .Include(c => c.Donatee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campaignDonatee == null)
            {
                return NotFound();
            }

            return View(campaignDonatee);
        }

        // POST: CampaignDonatees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var campaignDonatee = await _context.CampaignDonatees.FindAsync(id);
            _context.CampaignDonatees.Remove(campaignDonatee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignDonateeExists(Guid id)
        {
            return _context.CampaignDonatees.Any(e => e.Id == id);
        }
    }
}
