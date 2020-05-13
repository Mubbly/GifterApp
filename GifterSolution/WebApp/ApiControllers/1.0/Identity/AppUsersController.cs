// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using DAL.App.EF;
// using Domain.App.Identity;
//
// namespace WebApp.ApiControllers.Identity
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     [ApiVersion("1.0")]
//     public class AppUsersController : ControllerBase
//     {
//         private readonly AppDbContext _context;
//
//         public AppUsersController(AppDbContext context)
//         {
//             _context = context;
//         }
//
//         // GET: api/AppUsers
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
//         {
//             return await _context.Users.ToListAsync();
//         }
//
//         // GET: api/AppUsers/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<AppUser>> GetAppUser(Guid id)
//         {
//             var appUser = await _context.Users.FindAsync(id);
//
//             if (appUser == null)
//             {
//                 return NotFound();
//             }
//
//             return appUser;
//         }
//
//         // PUT: api/AppUsers/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutAppUser(Guid id, AppUser appUser)
//         {
//             if (id != appUser.Id)
//             {
//                 return BadRequest();
//             }
//
//             _context.Entry(appUser).State = EntityState.Modified;
//
//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!AppUserExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }
//
//             return NoContent();
//         }
//
//         // POST: api/AppUsers
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         [HttpPost]
//         public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
//         {
//             _context.Users.Add(appUser);
//             await _context.SaveChangesAsync();
//
//             return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
//         }
//
//         // DELETE: api/AppUsers/5
//         [HttpDelete("{id}")]
//         public async Task<ActionResult<AppUser>> DeleteAppUser(Guid id)
//         {
//             var appUser = await _context.Users.FindAsync(id);
//             if (appUser == null)
//             {
//                 return NotFound();
//             }
//
//             _context.Users.Remove(appUser);
//             await _context.SaveChangesAsync();
//
//             return appUser;
//         }
//
//         private bool AppUserExists(Guid id)
//         {
//             return _context.Users.Any(e => e.Id == id);
//         }
//     }
// }

