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
    public class CampaignDonateesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public CampaignDonateesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/CampaignDonatees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDonateeDTO>>> GetCampaignDonatees()
        {
            return Ok(await _uow.CampaignDonatees.DTOAllAsync());
        }

        // GET: api/CampaignDonatees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDonateeDTO>> GetCampaignDonatee(Guid id)
        {
            var campaignDonatee = await _uow.CampaignDonatees.DTOFirstOrDefaultAsync(id);
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
