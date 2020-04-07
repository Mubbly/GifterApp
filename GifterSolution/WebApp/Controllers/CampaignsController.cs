using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace WebApp.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CampaignsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        } 

        // GET: Campaigns
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Campaigns.AllAsync());
        }

        // GET: Campaigns/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,AdImage,Institution,ActiveFromDate,ActiveToDate,IsActive,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                //campaign.Id = Guid.NewGuid();
                _uow.Campaigns.Add(campaign);
                await _uow.Campaigns.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,AdImage,Institution,ActiveFromDate,ActiveToDate,IsActive,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(campaign);
            }
            _uow.Campaigns.Update(campaign);
            await _uow.Campaigns.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Campaigns/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _uow.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.Campaigns.Remove(id);
            await _uow.Campaigns.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
