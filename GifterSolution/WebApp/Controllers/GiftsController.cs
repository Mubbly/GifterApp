using System;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class GiftsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IGiftRepository _giftRepository;

        public GiftsController(AppDbContext context)
        {
            _context = context;
            _giftRepository = new GiftRepository(context);
        } 

        // GET: Gifts
        public async Task<IActionResult> Index()
        {
            return View(await _giftRepository.AllAsync());
        }

        // GET: Gifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftRepository.FindAsyncLala(id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // GET: Gifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Image,Url,PartnerUrl,IsPartnered,IsPinned,ActionTypeId,AppUserId,StatusId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                //gift.Id = Guid.NewGuid(); // Probably not needed, already works?
                _giftRepository.Add(gift);
                await _giftRepository.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(gift);
        }

        // GET: Gifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftRepository.FindAsyncLala(id);
            if (gift == null)
            {
                return NotFound();
            }
            return View(gift);
        }

        // POST: Gifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Image,Url,PartnerUrl,IsPartnered,IsPinned,ActionTypeId,AppUserId,StatusId,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] Gift gift)
        {
            if (id != gift.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(gift);
            }
            
            // TODO: Validation in repository (this previously had try-catch, using "doesGiftExist" method, but shouldn't be done here
            _giftRepository.Update(gift);
            await _giftRepository.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }

        // GET: Gifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gift = await _giftRepository.FindAsyncLala(id);
            if (gift == null)
            {
                return NotFound();
            }

            return View(gift);
        }

        // POST: Gifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _giftRepository.Remove(id);
            await _giftRepository.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
