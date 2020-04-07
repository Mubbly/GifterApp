// using System;
// using System.Threading.Tasks;
// using Contracts.DAL.App;
// using Microsoft.AspNetCore.Mvc;
// using Domain.Identity;
//
// namespace WebApp.Controllers.Identity
// {
//     public class AppUsersController : Controller
//     {
//         private readonly IAppUnitOfWork _uow;
//
//         // TODO: How to approach Identity repos/use UserManager?
//         public AppUsersController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//         } 
//
//         // GET: AppUsers
//         public async Task<IActionResult> Index()
//         {
//             return View(await _uow.AppUsers.AllAsync());
//         }
//
//         // GET: AppUsers/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var appUser = await _uow.AppUsers.FindAsync(id);
//             if (appUser == null)
//             {
//                 return NotFound();
//             }
//
//             return View(appUser);
//         }
//
//         // GET: AppUsers/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: AppUsers/Create
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("FirstName,LastName,IsCampaignManager,IsActive,LastActive,DateJoined,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
//         {
//             if (ModelState.IsValid)
//             {
//                 //appUser.Id = Guid.NewGuid();
//                 _uow.AppUsers.Add(appUser);
//                 await _uow.AppUsers.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(appUser);
//         }
//
//         // GET: AppUsers/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var appUser = await _uow.AppUsers.FindAsync(id);
//             if (appUser == null)
//             {
//                 return NotFound();
//             }
//             return View(appUser);
//         }
//
//         // POST: AppUsers/Edit/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,IsCampaignManager,IsActive,LastActive,DateJoined,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] AppUser appUser)
//         {
//             if (id != appUser.Id)
//             {
//                 return NotFound();
//             }
//
//             if (!ModelState.IsValid)
//             {
//                 return View(appUser);
//             }
//             
//             _uow.AppUsers.Update(appUser);
//             await _uow.AppUsers.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         // GET: AppUsers/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var appUser = await _uow.AppUsers.FindAsync(id);
//             if (appUser == null)
//             {
//                 return NotFound();
//             }
//
//             return View(appUser);
//         }
//
//         // POST: AppUsers/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             _uow.AppUsers.Remove(id);
//             await _uow.AppUsers.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
