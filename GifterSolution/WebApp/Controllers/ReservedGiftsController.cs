// #pragma warning disable 1591
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using DAL.App.EF;
// using Domain.App;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
//
// namespace WebApp.Controllers
// {
//     public class ReservedGiftsController : Controller
//     {
//         private readonly AppDbContext _context;
//
//         public ReservedGiftsController(AppDbContext context)
//         {
//             _context = context;
//         }
//
//         // GET: ReservedGifts
//         public async Task<IActionResult> Index()
//         {
//             var appDbContext = _context.ReservedGifts.Include(r => r.ActionType).Include(r => r.Gift).Include(r => r.Status).Include(r => r.UserGiver).Include(r => r.UserReceiver);
//             return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: ReservedGifts/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var reservedGift = await _context.ReservedGifts
//                 .Include(r => r.ActionType)
//                 .Include(r => r.AppUser)
//                 .Include(r => r.Gift)
//                 .Include(r => r.Status)
//                 .Include(r => r.UserGiver)
//                 .Include(r => r.UserReceiver)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (reservedGift == null)
//             {
//                 return NotFound();
//             }
//
//             return View(reservedGift);
//         }
//
//         // GET: ReservedGifts/Create
//         public IActionResult Create()
//         {
//             ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue");
//             ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
//             ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Name");
//             ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue");
//             ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "FirstName");
//             ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "FirstName");
//             return View();
//         }
//
//         // POST: ReservedGifts/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("ReservedFrom,Comment,GiftId,ActionTypeId,StatusId,UserGiverId,UserReceiverId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] ReservedGift reservedGift)
//         {
//             if (ModelState.IsValid)
//             {
//                 reservedGift.Id = Guid.NewGuid();
//                 _context.Add(reservedGift);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", reservedGift.ActionTypeId);
//             ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.AppUserId);
//             ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Name", reservedGift.GiftId);
//             ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", reservedGift.StatusId);
//             ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserGiverId);
//             ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserReceiverId);
//             return View(reservedGift);
//         }
//
//         // GET: ReservedGifts/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var reservedGift = await _context.ReservedGifts.FindAsync(id);
//             if (reservedGift == null)
//             {
//                 return NotFound();
//             }
//             ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", reservedGift.ActionTypeId);
//             ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.AppUserId);
//             ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Name", reservedGift.GiftId);
//             ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", reservedGift.StatusId);
//             ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserGiverId);
//             ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserReceiverId);
//             return View(reservedGift);
//         }
//
//         // POST: ReservedGifts/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//         // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("ReservedFrom,Comment,GiftId,ActionTypeId,StatusId,UserGiverId,UserReceiverId,AppUserId,CreatedBy,CreatedAt,EditedBy,EditedAt,Id")] ReservedGift reservedGift)
//         {
//             if (id != reservedGift.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(reservedGift);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!ReservedGiftExists(reservedGift.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ActionTypeId"] = new SelectList(_context.ActionTypes, "Id", "ActionTypeValue", reservedGift.ActionTypeId);
//             ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.AppUserId);
//             ViewData["GiftId"] = new SelectList(_context.Gifts, "Id", "Name", reservedGift.GiftId);
//             ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "StatusValue", reservedGift.StatusId);
//             ViewData["UserGiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserGiverId);
//             ViewData["UserReceiverId"] = new SelectList(_context.Users, "Id", "FirstName", reservedGift.UserReceiverId);
//             return View(reservedGift);
//         }
//
//         // GET: ReservedGifts/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var reservedGift = await _context.ReservedGifts
//                 .Include(r => r.ActionType)
//                 .Include(r => r.AppUser)
//                 .Include(r => r.Gift)
//                 .Include(r => r.Status)
//                 .Include(r => r.UserGiver)
//                 .Include(r => r.UserReceiver)
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (reservedGift == null)
//             {
//                 return NotFound();
//             }
//
//             return View(reservedGift);
//         }
//
//         // POST: ReservedGifts/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             var reservedGift = await _context.ReservedGifts.FindAsync(id);
//             _context.ReservedGifts.Remove(reservedGift);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         private bool ReservedGiftExists(Guid id)
//         {
//             return _context.ReservedGifts.Any(e => e.Id == id);
//         }
//     }
// }
