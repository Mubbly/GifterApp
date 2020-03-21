using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories.Identity;
using Domain.Identity;

namespace WebApp.Controllers.Identity
{
    public class AppUsersController : Controller
    {
        private readonly IAppUserRepository _appUserRepository;

        // TODO: How to approach Identity repos?
        public AppUsersController(AppDbContext context)
        {
            _appUserRepository = new AppUserRepository(context);
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            return View(await _appUserRepository.AllAsync());
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _appUserRepository.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,IsCampaignManager,IsActive,LastActive,DateJoined,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                //appUser.Id = Guid.NewGuid();
                _appUserRepository.Add(appUser);
                await _appUserRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _appUserRepository.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,IsCampaignManager,IsActive,LastActive,DateJoined,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(appUser);
            }
            
            _appUserRepository.Update(appUser);
            await _appUserRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _appUserRepository.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _appUserRepository.Remove(id);
            await _appUserRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
