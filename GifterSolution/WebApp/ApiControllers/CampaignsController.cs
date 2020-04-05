using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
    public class CampaignsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public CampaignsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDTO>>> GetCampaigns()
        {
            return Ok(await _uow.Campaigns.DTOAllAsync());
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDTO>> GetCampaign(Guid id)
        {
            var campaign = await _uow.Campaigns.DTOFirstOrDefaultAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return campaign;
        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(Guid id, CampaignEditDTO campaignEditDTO)
        {
            if (id != campaignEditDTO.Id)
            {
                return BadRequest();
            }

            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(campaignEditDTO.Id);
            if (campaign == null)
            {
                return BadRequest();
            }
            campaign.Name = campaignEditDTO.Name;
            campaign.Description = campaignEditDTO.Description;
            campaign.AdImage = campaignEditDTO.AdImage;
            campaign.Institution = campaignEditDTO.Institution;
            campaign.ActiveFromDate = campaignEditDTO.ActiveFromDate;
            campaign.ActiveToDate = campaignEditDTO.ActiveToDate;

            _uow.Campaigns.Update(campaign);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Campaigns.ExistsAsync(id))
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

        // POST: api/Campaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignCreateDTO campaignCreateDTO)
        {
            var campaign = new Campaign
            {
                Name = campaignCreateDTO.Name,
                Description = campaignCreateDTO.Description,
                AdImage = campaignCreateDTO.AdImage,
                Institution = campaignCreateDTO.Institution,
                ActiveFromDate = campaignCreateDTO.ActiveFromDate,
                ActiveToDate = campaignCreateDTO.ActiveToDate
            };
            
            _uow.Campaigns.Add(campaign);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetCampaign", new { id = campaign.Id }, campaign);
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        {
            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            _uow.Campaigns.Remove(campaign);
            await _uow.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
