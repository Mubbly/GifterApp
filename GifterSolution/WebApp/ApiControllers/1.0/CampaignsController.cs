using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<CampaignsController> _logger;
        private readonly CampaignMapper _mapper = new CampaignMapper();

        public CampaignsController(IAppBLL appBLL, ILogger<CampaignsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Campaigns
        /// <summary>
        ///     Get all Campaigns
        /// </summary>
        /// <returns>List of Campaigns</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.CampaignDTO>>> GetCampaigns()
        {
            return Ok((await _bll.Campaigns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Campaigns/5
        /// <summary>
        ///     Get a single Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Campaign object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.CampaignDTO>> GetCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id); 
            if (campaign == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Campaign with id {id} not found"));
            }

            return Ok(_mapper.Map(campaign));
        }
        
        // GET: api/Campaigns/Personal
        /// <summary>
        ///     Get all personal Campaigns
        /// </summary>
        /// <returns>List of personal Campaigns</returns>
        [HttpGet("personal")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "campaignManager")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.CampaignDTO>>> GetPersonalCampaigns()
        {
            var personalCampaigns = await _bll.Campaigns.GetAllPersonalAsync(User.UserGuidId());
            return Ok(personalCampaigns.Select(e => _mapper.Map(e)));
        }
        
        // GET: api/Campaigns/Personal/5
        /// <summary>
        ///     Get a single personal Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns>personal Campaign object</returns>
        [HttpGet("personal/{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "campaignManager")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.CampaignDTO>> GetPersonalCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.GetPersonalAsync(id, User.UserGuidId());
            if (campaign == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Campaign with id {id} not found"));
            }

            return Ok(_mapper.Map(campaign));
        }
        
        // PUT: api/Campaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="campaignDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "campaignManager")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutCampaign(Guid id, V1DTO.CampaignDTO campaignDTO)
        {
            // Don't allow wrong data
            if (id != campaignDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and campaign.id do not match"));
            }
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(campaignDTO.Id, User.UserGuidId()); // TODO: Get personal campaign
            if (campaign == null)
            {
                _logger.LogError($"EDIT. No such campaign for id: {campaignDTO.Id} and user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Campaign found for this user with id {id}"));
            }
            // Update existing campaign
            // campaign.CampaignValue = campaignEditDTO.CampaignValue;
            // campaign.Comment = campaignEditDTO.Comment;
            await _bll.Campaigns.UpdateAsync(_mapper.Map(campaignDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Campaigns.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Campaign does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
            
            // Only allow campaignManagers to edit their own campaigns
            // var currentUserCampaigns = await _uow.UserCampaigns.DTOAllAsync(User.UserGuidId());
            // if (currentUserCampaigns == null)
            // {
            //     _logger.LogError($"EDIT. This user does not have any campaigns: {User.UserGuidId()}");
            //     return StatusCode(403);
            // }
            // if (currentUserCampaigns.All(uc => uc.CampaignId != id))
            // {
            //     _logger.LogError($"EDIT. This user does not own this campaign: {id}, user: {User.UserGuidId()}");
            //     return StatusCode(403);
            // }
        }

        // POST: api/Campaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Campaign
        /// </summary>
        /// <param name="campaignDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "campaignManager")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.CampaignDTO))]
        public async Task<ActionResult<V1DTO.CampaignDTO>> PostCampaign(V1DTO.CampaignDTO campaignDTO)
        {
            // Create campaign
            var bllEntity = _mapper.Map(campaignDTO);
            _logger.LogInformation($"Id: {User.UserGuidId()}");
            _bll.Campaigns.Add(bllEntity, User.UserGuidId());
            
            // Save to db
            await _bll.SaveChangesAsync();

            campaignDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetCampaign",
                new {id = campaignDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                campaignDTO);
            
            // Create UserCampaign table entry to connect campaign to user
            // var userCampaign = new UserCampaign
            // {
            //     AppUserId = User.UserGuidId(),
            //     CampaignId = campaign.Id
            // };
            // _uow.UserCampaigns.Add(userCampaign);

        }

        // DELETE: api/Campaigns/5
        /// <summary>
        ///     Delete Campaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "campaignManager")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.CampaignDTO>> DeleteCampaign(Guid id)
        {
            // Get campaign
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id, User.UserGuidId()); // TODO: Get personal campaign
            if (campaign == null)
            {
                _logger.LogError($"DELETE. No such campaign: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Campaign with id {id} not found"));
            }
            // Delete campaign
            await _bll.Campaigns.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(campaign);
        }

        // // GET: api/Campaigns
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.CampaignDTO>>> GetCampaigns()
        // {
        //     return Ok(await _uow.Campaigns.DTOAllAsync());
        // }
        //
        // // GET: api/Campaigns/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.CampaignDTO>> GetCampaign(Guid id)
        // {
        //     var campaign = await _uow.Campaigns.DTOFirstOrDefaultAsync(id);
        //
        //     if (campaign == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return campaign;
        // }
        //
        // // PUT: api/Campaigns/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutCampaign(Guid id, CampaignEditDTO campaignEditDTO)
        // {
        //     if (id != campaignEditDTO.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     var campaign = await _uow.Campaigns.FirstOrDefaultAsync(campaignEditDTO.Id);
        //     if (campaign == null)
        //     {
        //         return BadRequest();
        //     }
        //     campaign.Name = campaignEditDTO.Name;
        //     campaign.Description = campaignEditDTO.Description;
        //     campaign.AdImage = campaignEditDTO.AdImage;
        //     campaign.Institution = campaignEditDTO.Institution;
        //     campaign.ActiveFromDate = campaignEditDTO.ActiveFromDate;
        //     campaign.ActiveToDate = campaignEditDTO.ActiveToDate;
        //
        //     _uow.Campaigns.Update(campaign);
        //     
        //     try
        //     {
        //         await _uow.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!await _uow.Campaigns.ExistsAsync(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // POST: api/Campaigns
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Campaign>> PostCampaign(CampaignCreateDTO campaignCreateDTO)
        // {
        //     var campaign = new Campaign
        //     {
        //         Name = campaignCreateDTO.Name,
        //         Description = campaignCreateDTO.Description,
        //         AdImage = campaignCreateDTO.AdImage,
        //         Institution = campaignCreateDTO.Institution,
        //         ActiveFromDate = campaignCreateDTO.ActiveFromDate,
        //         ActiveToDate = campaignCreateDTO.ActiveToDate
        //     };
        //     
        //     _uow.Campaigns.Add(campaign);
        //     
        //     await _uow.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetCampaign", new { id = campaign.Id }, campaign);
        // }
        //
        // // DELETE: api/Campaigns/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Campaign>> DeleteCampaign(Guid id)
        // {
        //     var campaign = await _uow.Campaigns.FirstOrDefaultAsync(id);
        //     if (campaign == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _uow.Campaigns.Remove(campaign);
        //     await _uow.SaveChangesAsync();
        //
        //     return Ok(campaign);
        // }
    }
}
