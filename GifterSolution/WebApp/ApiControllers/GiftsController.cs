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
    public class GiftsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public GiftsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Gifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTO>>> GetGifts()
        {
            return Ok(await _uow.Gifts.DTOAllAsync());
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDTO>> GetGift(Guid id)
        {
            var gift = await _uow.Gifts.DTOFirstOrDefaultAsync(id);

            if (gift == null)
            {
                return NotFound();
            }

            return gift;
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

            var gift = await _uow.Gifts.FirstOrDefaultAsync(giftEditDTO.Id);
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

            _uow.Gifts.Update(gift);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Gifts.ExistsAsync(id))
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
            var gift = new Gift();
            
            gift.Name = giftCreateDTO.Name;
            gift.Description = giftCreateDTO.Description;
            gift.Image = giftCreateDTO.Image;
            gift.Url = giftCreateDTO.Url;
            gift.IsPartnered = giftCreateDTO.IsPartnered;
            gift.PartnerUrl = giftCreateDTO.PartnerUrl;
            gift.IsPinned = giftCreateDTO.IsPinned;
            gift.ActionTypeId = giftCreateDTO.ActionTypeId;
            gift.StatusId = giftCreateDTO.StatusId;
            gift.AppUserId = giftCreateDTO.AppUserId;
            
            _uow.Gifts.Add(gift);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetGift", new { id = gift.Id }, gift);
        }

        // DELETE: api/Gifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gift>> DeleteGift(Guid id)
        {
            var gift = await _uow.Gifts.FirstOrDefaultAsync(id);
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
