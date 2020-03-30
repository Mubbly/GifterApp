using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPermissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPermissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPermissionDTO>>> GetUserPermissions()
        {
            return await _context.UserPermissions
                .Select(up => new UserPermissionDTO()
                {
                    Id = up.Id,
                    Comment = up.Comment,
                    From = up.From,
                    To = up.To,
                    PermissionId = up.PermissionId,
                    AppUserId = up.AppUserId
                }).ToListAsync();
        }

        // GET: api/UserPermissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPermissionDTO>> GetUserPermission(Guid id)
        {
            var userPermission = await _context.UserPermissions
                .Select(up => new UserPermissionDTO()
                {
                    Id = up.Id,
                    Comment = up.Comment,
                    From = up.From,
                    To = up.To,
                    PermissionId = up.PermissionId,
                    AppUserId = up.AppUserId
                }).SingleOrDefaultAsync();

            if (userPermission == null)
            {
                return NotFound();
            }

            return userPermission;
        }

        // PUT: api/UserPermissions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPermission(Guid id, UserPermission userPermission)
        {
            if (id != userPermission.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPermission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPermissionExists(id))
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

        // POST: api/UserPermissions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserPermission>> PostUserPermission(UserPermission userPermission)
        {
            _context.UserPermissions.Add(userPermission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPermission", new { id = userPermission.Id }, userPermission);
        }

        // DELETE: api/UserPermissions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPermission>> DeleteUserPermission(Guid id)
        {
            var userPermission = await _context.UserPermissions.FindAsync(id);
            if (userPermission == null)
            {
                return NotFound();
            }

            _context.UserPermissions.Remove(userPermission);
            await _context.SaveChangesAsync();

            return userPermission;
        }

        private bool UserPermissionExists(Guid id)
        {
            return _context.UserPermissions.Any(e => e.Id == id);
        }
    }
}
