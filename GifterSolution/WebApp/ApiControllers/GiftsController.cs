using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    // [ApiVersion("1.0")]
    // [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GiftsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GiftsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Gifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gift>>> GetGifts()
        {
            return await _context.Gifts
                .Where(g => g.AppUserId == User.UserGuidId())
                .ToListAsync();
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gift>> GetGift(Guid id)
        {
            var gift = await _context.Gifts
                .FirstOrDefaultAsync(g => g.Id == id && g.AppUserId == User.UserGuidId());

            if (gift == null)
            {
                return NotFound();
            }

            return gift;
        }

        // PUT: api/Gifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGift(Guid id, Gift gift)
        {
            if (id != gift.Id)
            {
                return BadRequest();
            }

            _context.Entry(gift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiftExists(id))
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

        // POST: api/Gifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Gift>> PostGift(Gift gift)
        {
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGift", new { id = gift.Id }, gift);
        }

        // DELETE: api/Gifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gift>> DeleteGift(Guid id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return NotFound();
            }

            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();

            return gift;
        }

        private bool GiftExists(Guid id)
        {
            return _context.Gifts.Any(e => e.Id == id);
        }
    }
}
