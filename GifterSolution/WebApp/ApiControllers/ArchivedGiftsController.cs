using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivedGiftsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ArchivedGiftsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/ArchivedGifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArchivedGiftDTO>>> GetArchivedGifts()
        {
            return Ok(await _uow.ArchivedGifts.DTOAllAsync());
        }

        // GET: api/ArchivedGifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivedGiftDTO>> GetArchivedGift(Guid id)
        {
            var archivedGift = await _uow.ArchivedGifts.DTOFirstOrDefaultAsync(id);
            
            if (archivedGift == null)
            {
                return NotFound();
            }

            return archivedGift;
        }

        // PUT: api/ArchivedGifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchivedGift(Guid id, ArchivedGift archivedGift)
        {
            if (id != archivedGift.Id)
            {
                return BadRequest();
            }

            _context.Entry(archivedGift).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchivedGiftExists(id))
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

        // POST: api/ArchivedGifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ArchivedGift>> PostArchivedGift(ArchivedGift archivedGift)
        {
            _context.ArchivedGifts.Add(archivedGift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchivedGift", new { id = archivedGift.Id }, archivedGift);
        }

        // DELETE: api/ArchivedGifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArchivedGift>> DeleteArchivedGift(Guid id)
        {
            var archivedGift = await _context.ArchivedGifts.FindAsync(id);
            if (archivedGift == null)
            {
                return NotFound();
            }

            _context.ArchivedGifts.Remove(archivedGift);
            await _context.SaveChangesAsync();

            return archivedGift;
        }

        private bool ArchivedGiftExists(Guid id)
        {
            return _context.ArchivedGifts.Any(e => e.Id == id);
        }
    }
}
