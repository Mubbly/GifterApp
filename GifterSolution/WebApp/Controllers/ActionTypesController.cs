using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class ActionTypesController : Controller
    {
        private readonly IActionTypeRepository _actionTypeRepository;

        public ActionTypesController(AppDbContext context)
        {
            _actionTypeRepository = new ActionTypeRepository(context);
        }

        // GET: ActionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _actionTypeRepository.AllAsync());
        }

        // GET: ActionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _actionTypeRepository.FindAsync(id);
            if (actionType == null)
            {
                return NotFound();
            }

            return View(actionType);
        }

        // GET: ActionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActionTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] ActionType actionType)
        {
            if (ModelState.IsValid)
            {
                //actionType.Id = Guid.NewGuid();
                _actionTypeRepository.Add(actionType);
                await _actionTypeRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actionType);
        }

        // GET: ActionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _actionTypeRepository.FindAsync(id);
            if (actionType == null)
            {
                return NotFound();
            }
            return View(actionType);
        }

        // POST: ActionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ActionTypeValue,Comment,Id,CreatedBy,CreatedAt,EditedBy,EditedAt")] ActionType actionType)
        {
            if (id != actionType.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(actionType);
            }
            
            _actionTypeRepository.Update(actionType);
            await _actionTypeRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ActionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionType = await _actionTypeRepository.FindAsync(id);
            if (actionType == null)
            {
                return NotFound();
            }

            return View(actionType);
        }

        // POST: ActionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _actionTypeRepository.Remove(id);
            await _actionTypeRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}