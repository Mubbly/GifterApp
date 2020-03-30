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
    public class ReservedGiftsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservedGiftsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservedGifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservedGiftDTO>>> GetReservedGifts()
        {
            return await _context.ReservedGifts
                .Select(rg => new ReservedGiftDTO() 
                {
                    Id = rg.Id,
                    Comment = rg.Comment,
                    GiftId = rg.GiftId,
                    ReservedFrom = rg.ReservedFrom,
                    StatusId = rg.StatusId,
                    ActionTypeId = rg.ActionTypeId,
                    UserGiverId = rg.UserGiverId,
                    UserReceiverId = rg.UserReceiverId
                }).ToListAsync();
        }

        // GET: api/ReservedGifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservedGiftDTO>> GetReservedGift(Guid id)
        {
            var reservedGift = await _context.ReservedGifts
                .Select(rg => new ReservedGiftDTO() 
                {
                    Id = rg.Id,
                    Comment = rg.Comment,
                    GiftId = rg.GiftId,
                    ReservedFrom = rg.ReservedFrom,
                    StatusId = rg.StatusId,
                    ActionTypeId = rg.ActionTypeId,
                    UserGiverId = rg.UserGiverId,
                    UserReceiverId = rg.UserReceiverId
                }).SingleOrDefaultAsync();

            if (reservedGift == null)
            {
                return NotFound();
            }

            return reservedGift;
        }

        // PUT: api/ReservedGifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservedGift(Guid id, ReservedGift reservedGift)
        {
            if (id != reservedGift.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservedGift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservedGiftExists(id))
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

        // POST: api/ReservedGifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReservedGift>> PostReservedGift(ReservedGift reservedGift)
        {
            _context.ReservedGifts.Add(reservedGift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservedGift", new { id = reservedGift.Id }, reservedGift);
        }

        // DELETE: api/ReservedGifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservedGift>> DeleteReservedGift(Guid id)
        {
            var reservedGift = await _context.ReservedGifts.FindAsync(id);
            if (reservedGift == null)
            {
                return NotFound();
            }

            _context.ReservedGifts.Remove(reservedGift);
            await _context.SaveChangesAsync();

            return reservedGift;
        }

        private bool ReservedGiftExists(Guid id)
        {
            return _context.ReservedGifts.Any(e => e.Id == id);
        }
    }
}
