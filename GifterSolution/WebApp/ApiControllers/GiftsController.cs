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
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    // [ApiVersion("1.0")]
    // [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GiftsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<CampaignsController> _logger;

        public GiftsController(IAppUnitOfWork uow, ILogger<CampaignsController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET: api/Gifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTO>>> GetGifts()
        {
            // Allow all users see all gifts
            return Ok(await _uow.Gifts.DTOAllAsync());
        }
        
        // GET: api/Gifts/Personal
        [HttpGet("personal")]
        public async Task<ActionResult<IEnumerable<GiftDTO>>> GetPersonalGifts()
        {
            // Get only your own gifts
            return Ok(await _uow.Gifts.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDTO>> GetGift(Guid id)
        {
            // Allow all users see all gifts
            var giftDTO = await _uow.Gifts.DTOFirstOrDefaultAsync(id);
            if (giftDTO == null)
            {
                return NotFound();
            }
            return Ok(giftDTO);
        }

        // PUT: api/Gifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGift(Guid id, GiftEditDTO giftEditDTO)
        {
            if (id != giftEditDTO.Id)
            {
                return BadRequest();
            }
            
            // Only allow users to edit their own gifts
            var gift = await _uow.Gifts.FirstOrDefaultAsync(giftEditDTO.Id, User.UserGuidId());
            if (gift == null)
            {
                return BadRequest();
            }
            gift.Name = giftEditDTO.Name;
            gift.Description = giftEditDTO.Description;
            gift.Image = giftEditDTO.Image;
            gift.Url = giftEditDTO.Url;
            gift.IsPartnered = giftEditDTO.IsPartnered;
            gift.PartnerUrl = giftEditDTO.PartnerUrl;
            gift.IsPinned = giftEditDTO.IsPinned;
            gift.ActionTypeId = giftEditDTO.ActionTypeId;
            gift.StatusId = giftEditDTO.StatusId;
            gift.AppUserId = giftEditDTO.AppUserId;
            gift.WishlistId = giftEditDTO.WishlistId;

            _uow.Gifts.Update(gift);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Gifts.ExistsAsync(id, User.UserGuidId()))
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
        public async Task<ActionResult<GiftCreateDTO>> PostGift(GiftCreateDTO giftCreateDTO)
        {
            // Allow all users create gifts
            var gift = new Gift
            {
                Name = giftCreateDTO.Name,
                Description = giftCreateDTO.Description,
                Image = giftCreateDTO.Image,
                Url = giftCreateDTO.Url,
                IsPartnered = giftCreateDTO.IsPartnered,
                PartnerUrl = giftCreateDTO.PartnerUrl,
                IsPinned = giftCreateDTO.IsPinned,
                ActionTypeId = giftCreateDTO.ActionTypeId,
                StatusId = giftCreateDTO.StatusId,
                AppUserId = User.UserGuidId()
            };
            
            _uow.Gifts.Add(gift);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetGift", new { id = gift.Id }, gift);
        }

        // DELETE: api/Gifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gift>> DeleteGift(Guid id)
        {
            // Only allow users to delete their own gifts
            var gift = await _uow.Gifts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (gift == null)
            {
                return NotFound();
            }
            _uow.Gifts.Remove(gift);
            await _uow.SaveChangesAsync();

            return Ok(gift);
        }
    }
}
