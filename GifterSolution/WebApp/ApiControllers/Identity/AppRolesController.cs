using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.Identity;

namespace WebApp.ApiControllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AppRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/AppRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> GetAppRole(Guid id)
        {
            var appRole = await _context.Roles.FindAsync(id);

            if (appRole == null)
            {
                return NotFound();
            }

            return appRole;
        }

        // PUT: api/AppRoles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppRole(Guid id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(appRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppRoles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AppRole>> PostAppRole(AppRole appRole)
        {
            _context.Roles.Add(appRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppRole", new { id = appRole.Id }, appRole);
        }

        // DELETE: api/AppRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppRole>> DeleteAppRole(Guid id)
        {
            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(appRole);
            await _context.SaveChangesAsync();

            return appRole;
        }

        private bool AppRoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
