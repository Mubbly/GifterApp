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
    public class UserCampaignsController : Controller
    {
        private readonly AppDbContext _context;

        public UserCampaignsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserCampaigns
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserCampaigns.Include(u => u.AppUser).Include(u => u.Campaign);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserCampaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCampaign = await _context.UserCampaigns
                .Include(u => u.AppUser)
                .Include(u => u.Campaign)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCampaign == null)
            {
                return NotFound();
            }

            return View(userCampaign);
        }

        // GET: UserCampaigns/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name");
            return View();
        }

        // POST: UserCampaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,AppUserId,CampaignId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] UserCampaign userCampaign)
        {
            if (ModelState.IsValid)
            {
                userCampaign.Id = Guid.NewGuid();
                _context.Add(userCampaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCampaign.AppUserId);
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", userCampaign.CampaignId);
            return View(userCampaign);
        }

        // GET: UserCampaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCampaign = await _context.UserCampaigns.FindAsync(id);
            if (userCampaign == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCampaign.AppUserId);
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", userCampaign.CampaignId);
            return View(userCampaign);
        }

        // POST: UserCampaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Comment,AppUserId,CampaignId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] UserCampaign userCampaign)
        {
            if (id != userCampaign.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCampaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCampaignExists(userCampaign.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", userCampaign.AppUserId);
            ViewData["CampaignId"] = new SelectList(_context.Campaigns, "Id", "Name", userCampaign.CampaignId);
            return View(userCampaign);
        }

        // GET: UserCampaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCampaign = await _context.UserCampaigns
                .Include(u => u.AppUser)
                .Include(u => u.Campaign)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userCampaign == null)
            {
                return NotFound();
            }

            return View(userCampaign);
        }

        // POST: UserCampaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userCampaign = await _context.UserCampaigns.FindAsync(id);
            _context.UserCampaigns.Remove(userCampaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCampaignExists(Guid id)
        {
            return _context.UserCampaigns.Any(e => e.Id == id);
        }
    }
}
