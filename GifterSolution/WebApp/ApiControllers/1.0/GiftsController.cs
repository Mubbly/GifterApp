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
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GiftsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<GiftsController> _logger;
        private readonly GiftMapper _mapper = new GiftMapper();

        public GiftsController(IAppBLL appBLL, ILogger<GiftsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }

        // GET: api/Gifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetGifts()
        {
            return Ok((await _bll.Gifts.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetGift(Guid id)
        {
            var gift = await _bll.Gifts.FirstOrDefaultAsync(id);
            if (gift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
            }

            return Ok(_mapper.Map(gift));
        }

        // PUT: api/Gifts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGift(Guid id, V1DTO.GiftDTO giftDTO)
        {
            // Don't allow wrong data
            if (id != giftDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and gift.id do not match"));
            }
            var gift = await _bll.Gifts.FirstOrDefaultAsync(giftDTO.Id, User.UserGuidId());
            if (gift == null)
            {
                _logger.LogError($"EDIT. No such gift: {giftDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Gift found for id {id}"));
            }
            // Update existing gift
            await _bll.Gifts.UpdateAsync(_mapper.Map(giftDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Gifts.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Gift does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Gifts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<V1DTO.GiftDTO>> PostGift(V1DTO.GiftDTO giftDTO)
        {
            // Create gift
            var bllEntity = _mapper.Map(giftDTO);
            _bll.Gifts.Add(bllEntity);
            // Save to db
            await _bll.SaveChangesAsync();

            giftDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetGift",
                new {id = giftDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                giftDTO
            );
        }

        // DELETE: api/Gifts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.GiftDTO>> DeleteGift(Guid id)
        {
            var gift = await _bll.Gifts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (gift == null)
            {
                _logger.LogError($"DELETE. No such gift: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
            }
            await _bll.Gifts.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(gift);
        }
    }
}
