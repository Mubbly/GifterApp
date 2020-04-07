using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatusesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatuses()
        {
            return await _context.Statuses
                .Select(s => new StatusDTO()
                {
                    Id = s.Id,
                    Comment = s.Comment,
                    DonateesCount = s.Donatees.Count,
                    GiftsCount = s.Gifts.Count,
                    StatusValue = s.StatusValue,
                    ArchivedGiftsCount = s.ArchivedGifts.Count,
                    ReservedGiftsCount = s.ReservedGifts.Count
                }).ToListAsync();
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> GetStatus(Guid id)
        {
            var status = await _context.Statuses
                .Select(s => new StatusDTO()
                {
                    Id = s.Id,
                    Comment = s.Comment,
                    DonateesCount = s.Donatees.Count,
                    GiftsCount = s.Gifts.Count,
                    StatusValue = s.StatusValue,
                    ArchivedGiftsCount = s.ArchivedGifts.Count,
                    ReservedGiftsCount = s.ReservedGifts.Count
                }).SingleOrDefaultAsync();

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Statuses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Status>> DeleteStatus(Guid id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return status;
        }

        private bool StatusExists(Guid id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}
