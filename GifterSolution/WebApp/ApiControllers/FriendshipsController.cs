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
    public class FriendshipsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FriendshipsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendshipDTO>>> GetFriendships()
        {
            return await _context.Friendships
                .Select(f => new FriendshipDTO()
                {
                    Id = f.Id,
                    Comment = f.Comment,
                    AppUser1Id = f.AppUser1Id,
                    AppUser2Id = f.AppUser2Id,
                    IsConfirmed = f.IsConfirmed
                }).ToListAsync();
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendshipDTO>> GetFriendship(Guid id)
        {
            var friendship = await _context.Friendships
                .Select(f => new FriendshipDTO()
                {
                    Id = f.Id,
                    Comment = f.Comment,
                    AppUser1Id = f.AppUser1Id,
                    AppUser2Id = f.AppUser2Id,
                    IsConfirmed = f.IsConfirmed
                }).SingleOrDefaultAsync();

            if (friendship == null)
            {
                return NotFound();
            }

            return friendship;
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(Guid id, Friendship friendship)
        {
            if (id != friendship.Id)
            {
                return BadRequest();
            }

            _context.Entry(friendship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
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

        // POST: api/Friendships
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendship", new { id = friendship.Id }, friendship);
        }

        // DELETE: api/Friendships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friendship>> DeleteFriendship(Guid id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return friendship;
        }

        private bool FriendshipExists(Guid id)
        {
            return _context.Friendships.Any(e => e.Id == id);
        }
    }
}