using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DonateesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DonateesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Donatees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donatee>>> GetDonatees()
        {
            return await _context.Donatees.ToListAsync();
        }

        // GET: api/Donatees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donatee>> GetDonatee(Guid id)
        {
            var donatee = await _context.Donatees.FindAsync(id);

            if (donatee == null)
            {
                return NotFound();
            }

            return donatee;
        }

        // PUT: api/Donatees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonatee(Guid id, Donatee donatee)
        {
            if (id != donatee.Id)
            {
                return BadRequest();
            }

            _context.Entry(donatee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonateeExists(id))
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

        // POST: api/Donatees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Donatee>> PostDonatee(Donatee donatee)
        {
            _context.Donatees.Add(donatee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonatee", new { id = donatee.Id }, donatee);
        }

        // DELETE: api/Donatees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Donatee>> DeleteDonatee(Guid id)
        {
            var donatee = await _context.Donatees.FindAsync(id);
            if (donatee == null)
            {
                return NotFound();
            }

            _context.Donatees.Remove(donatee);
            await _context.SaveChangesAsync();

            return donatee;
        }

        private bool DonateeExists(Guid id)
        {
            return _context.Donatees.Any(e => e.Id == id);
        }
    }
}
