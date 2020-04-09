using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishlistsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlists()
        {
            return await _context.Wishlists.ToListAsync();
        }

        // GET: api/Wishlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlist(Guid id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id);

            if (wishlist == null)
            {
                return NotFound();
            }

            return wishlist;
        }

        // PUT: api/Wishlists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishlist(Guid id, Wishlist wishlist)
        {
            if (id != wishlist.Id)
            {
                return BadRequest();
            }

            _context.Entry(wishlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishlistExists(id))
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

        // POST: api/Wishlists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWishlist", new { id = wishlist.Id }, wishlist);
        }

        // DELETE: api/Wishlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wishlist>> DeleteWishlist(Guid id)
        {
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();

            return wishlist;
        }

        private bool WishlistExists(Guid id)
        {
            return _context.Wishlists.Any(e => e.Id == id);
        }
    }
}
