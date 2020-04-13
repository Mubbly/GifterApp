using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;
using WebApp.ApiControllers.Identity;
using WebApp.Areas.Identity.Pages.Account;

namespace WebApp.ApiControllers
{
    /**
     * Everyone can see campaigns, only campaign managers can create and only creators can edit.
     */
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<CampaignsController> _logger;
        private readonly UserManager<AppUser> _userManager;
        
        public CampaignsController(IAppUnitOfWork uow, UserManager<AppUser> userManager, ILogger<CampaignsController> logger)
        {
            _uow = uow;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDTO>>> GetCampaigns()
        {
            // Allow all users see campaigns
            return Ok(await _uow.Campaigns.DTOAllAsync());
        }
        
        // GET: api/Campaigns/Personal
        [HttpGet("personal")]
        public async Task<ActionResult<IEnumerable<CampaignDTO>>> GetPersonalCampaigns()
        {
            var campaigns = new List<CampaignDTO>();
            
            // Only allow users who are campaign managers create new campaigns
            var currentUser = await _userManager.FindByIdAsync(User.UserId());
            if (currentUser == null || !currentUser.IsCampaignManager)
            {
                _logger.LogError($"GET/PERSONAL. This user is not a campaign manager: {User.UserGuidId()}");
                return StatusCode(403);
            }
            // Find IDs of current user's campaigns
            var currentUserCampaigns = await _uow.UserCampaigns.DTOAllAsync(User.UserGuidId());
            if (currentUserCampaigns == null)
            {
                _logger.LogWarning($"GET/PERSONAL. This user does not have any campaigns: {User.UserGuidId()}");
                return NotFound();
            }
            // Get only these campaigns
            foreach (var userCampaign in currentUserCampaigns)
            {
                var campaign = await _uow.Campaigns.DTOFirstOrDefaultAsync(userCampaign.CampaignId);
                if (campaign != null)
                {
                    campaigns.Add(campaign);
                }
            }
            return Ok(campaigns);
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignDTO>> GetCampaign(Guid id)
        {
            // Allow all users see campaigns
            var campaignDTO = await _uow.Campaigns.DTOFirstOrDefaultAsync(id);
            if (campaignDTO == null)
            {
                _logger.LogInformation($"GET/ID. No such campaign: {id}");
                return NotFound();
            }
            return Ok(campaignDTO);
        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(Guid id, CampaignEditDTO campaignEditDTO)
        {
            // Only allow users who are campaign managers edit campaigns
            var currentUser = await _userManager.FindByIdAsync(User.UserId());
            if (currentUser == null)
            {
                _logger.LogError($"EDIT. This user does not exist: {User.UserGuidId()}");
                return NotFound();
            }
            if (!currentUser.IsCampaignManager)
            {
                _logger.LogError($"EDIT. This user is not campaign manager: {User.UserGuidId()}");
                return StatusCode(403);
            }
            // Only allow users to edit their own campaigns
            var currentUserCampaigns = await _uow.UserCampaigns.DTOAllAsync(User.UserGuidId());
            if (currentUserCampaigns == null)
            {
                _logger.LogError($"EDIT. This user does not have any campaigns: {User.UserGuidId()}");
                return StatusCode(403);
            }
            if (currentUserCampaigns.All(uc => uc.CampaignId != id))
            {
                _logger.LogError($"EDIT. This user does not own this campaign: {id}, user: {User.UserGuidId()}");
                return StatusCode(403);
            }

            // Don't allow wrong data
            if (id != campaignEditDTO.Id)
            {
                _logger.LogError($"EDIT. Initial and edited campaign IDs don't match: {id}, {campaignEditDTO.Id}");
                return BadRequest();
            }
            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(campaignEditDTO.Id, User.UserGuidId());
            if (campaign == null)
            {
                _logger.LogError($"EDIT. No such campaign: {campaignEditDTO.Id}, user: {User.UserGuidId()}");
                return BadRequest();
            }
            
            // Update existing campaign
            campaign.Name = campaignEditDTO.Name;
            campaign.Description = campaignEditDTO.Description;
            campaign.AdImage = campaignEditDTO.AdImage;
            campaign.Institution = campaignEditDTO.Institution;
            campaign.ActiveFromDate = campaignEditDTO.ActiveFromDate;
            campaign.ActiveToDate = campaignEditDTO.ActiveToDate;

            _uow.Campaigns.Update(campaign);
            
            // Save to db
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Campaigns.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError($"EDIT. Campaign does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // POST: api/Campaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CampaignCreateDTO>> PostCampaign(CampaignCreateDTO campaignCreateDTO)
        {
            // Only allow users who are campaign managers create campaigns
            var currentUser = await _userManager.FindByIdAsync(User.UserId());
            if (currentUser == null)
            {
                _logger.LogError($"CREATE. This user does not exist: {User.UserGuidId()}");
                return NotFound();
            }
            if (!currentUser.IsCampaignManager)
            {
                _logger.LogError($"CREATE. This user is not campaign manager: {User.UserGuidId()}");
                return StatusCode(403);
            }
            
            // Create campaign
            var campaign = new Campaign
            {
                Name = campaignCreateDTO.Name,
                Description = campaignCreateDTO.Description,
                AdImage = campaignCreateDTO.AdImage,
                Institution = campaignCreateDTO.Institution,
                ActiveFromDate = campaignCreateDTO.ActiveFromDate,
                ActiveToDate = campaignCreateDTO.ActiveToDate,
            };
            _uow.Campaigns.Add(campaign);
            
            // Create UserCampaign table entry to connect campaign to user
            var userCampaign = new UserCampaign
            {
                AppUserId = User.UserGuidId(),
                CampaignId = campaign.Id
            };
            _uow.UserCampaigns.Add(userCampaign);
            
            // Save from memory to db
            await _uow.SaveChangesAsync();

            // Send response back to user
            campaignCreateDTO.Id = campaign.Id;
            return CreatedAtAction("GetCampaign", new { id = campaign.Id }, campaignCreateDTO);
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        {
            // Only allow users who are campaign managers delete campaigns
            var currentUser = await _userManager.FindByIdAsync(User.UserId());
            if (currentUser == null)
            {
                _logger.LogError($"DELETE. This user does not exist: {User.UserGuidId()}");
                return NotFound();
            }
            if (!currentUser.IsCampaignManager)
            {
                _logger.LogError($"DELETE. This user is not campaign manager: {User.UserGuidId()}");
                return BadRequest();
            }
            // Get campaign
            var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id, User.UserGuidId());
            if (campaign == null)
            {
                _logger.LogError($"DELETE. No such campaign: {id}, user: {User.UserGuidId()}");
                return NotFound();
            }
            // Delete campaign
            _uow.Campaigns.Remove(campaign);
            
            await _uow.SaveChangesAsync();
            return Ok(campaign);
        }
    }
}
