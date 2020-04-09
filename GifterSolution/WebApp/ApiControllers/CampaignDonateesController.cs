using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class CampaignDonateesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CampaignDonateesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CampaignDonatees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDonatee>>> GetCampaignDonatees()
        {
            return await _context.CampaignDonatees.ToListAsync();
        }

        // GET: api/CampaignDonatees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDonatee>> GetCampaignDonatee(Guid id)
        {
            var campaignDonatee = await _context.CampaignDonatees.FindAsync(id);

            if (campaignDonatee == null)
            {
                return NotFound();
            }

            return campaignDonatee;
        }

        // PUT: api/CampaignDonatees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaignDonatee(Guid id, CampaignDonatee campaignDonatee)
        {
            if (id != campaignDonatee.Id)
            {
                return BadRequest();
            }

            _context.Entry(campaignDonatee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignDonateeExists(id))
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

        // POST: api/CampaignDonatees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CampaignDonatee>> PostCampaignDonatee(CampaignDonatee campaignDonatee)
        {
            _context.CampaignDonatees.Add(campaignDonatee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampaignDonatee", new { id = campaignDonatee.Id }, campaignDonatee);
        }

        // DELETE: api/CampaignDonatees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CampaignDonatee>> DeleteCampaignDonatee(Guid id)
        {
            var campaignDonatee = await _context.CampaignDonatees.FindAsync(id);
            if (campaignDonatee == null)
            {
                return NotFound();
            }

            _context.CampaignDonatees.Remove(campaignDonatee);
            await _context.SaveChangesAsync();

            return campaignDonatee;
        }

        private bool CampaignDonateeExists(Guid id)
        {
            return _context.CampaignDonatees.Any(e => e.Id == id);
        }
    }
}
