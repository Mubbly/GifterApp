using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace WebApp.Controllers
{
    public class NotificationTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public NotificationTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        } 

        // GET: NotificationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.NotificationTypes.AllAsync());
        }

        // GET: NotificationTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _uow.NotificationTypes.FindAsync(id);
            if (notificationType == null)
            {
                return NotFound();
            }

            return View(notificationType);
        }

        // GET: NotificationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotificationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotificationTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] NotificationType notificationType)
        {
            if (ModelState.IsValid)
            {
                //notificationType.Id = Guid.NewGuid();
                _uow.NotificationTypes.Add(notificationType);
                await _uow.NotificationTypes.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notificationType);
        }

        // GET: NotificationTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _uow.NotificationTypes.FindAsync(id);
            if (notificationType == null)
            {
                return NotFound();
            }
            return View(notificationType);
        }

        // POST: NotificationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NotificationTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] NotificationType notificationType)
        {
            if (id != notificationType.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(notificationType);
            }
            _uow.NotificationTypes.Update(notificationType);
            await _uow.NotificationTypes.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: NotificationTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notificationType = await _uow.NotificationTypes.FindAsync(id);
            if (notificationType == null)
            {
                return NotFound();
            }

            return View(notificationType);
        }

        // POST: NotificationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _uow.NotificationTypes.Remove(id);
            await _uow.NotificationTypes.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
