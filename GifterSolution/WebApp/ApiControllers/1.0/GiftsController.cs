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
    /// <summary>
    ///     Gifts controller
    /// </summary>
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

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="appBLL"></param>
        /// <param name="logger"></param>
        public GiftsController(IAppBLL appBLL, ILogger<GiftsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }

        // GET: api/Gifts/User/5
        /// <summary>
        ///     Get all gifts for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetGifts(Guid userId)
        {
            var personalGifts = await _bll.Gifts.GetAllForUserAsync(userId);
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gifts not found"));
            }
            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }
        
        // GET: api/Gifts/Pinned/User/5
        /// <summary>
        ///     Get all gifts for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("pinned/user/{userId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetPinnedGifts(Guid userId)
        {
            var personalGifts = await _bll.Gifts.GetAllPinnedForUserAsync(userId);
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gifts not found"));
            }
            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }
        
        // GET: api/Gifts/Personal
        /// <summary>
        ///     Get all personal gifts
        /// </summary>
        /// <returns></returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetPersonalGifts()
        {
            var personalGifts = await _bll.Gifts.GetAllForUserAsync(User.UserGuidId());
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO("Gifts not found"));
            }
            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }

        // // GET: api/Gifts/5
        // /// <summary>
        // ///     Get a single gift
        // /// </summary>
        // /// <param name="id"></param>
        // /// <returns></returns>
        // [HttpGet("{id}")]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        // public async Task<ActionResult<V1DTO.GiftDTO>> GetGift(Guid id)
        // {
        //     var gift = await _bll.Gifts.FirstOrDefaultAsync(id);
        //     if (gift == null)
        //     {
        //         return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
        //     }
        //     return Ok(_mapper.Map(gift));
        // }
        
        // GET: api/Gifts/Personal/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("personal/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetPersonalGift(Guid id)
        {
            var gift = await _bll.Gifts.GetForUserAsync(id, User.UserGuidId());
            if (gift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
            }
            return Ok(_mapper.Map(gift));
        }

        // PUT: api/Gifts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Edit existing gift
        /// </summary>
        /// <param name="id"></param>
        /// <param name="giftDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<IActionResult> PutGift(Guid id, V1DTO.GiftDTO giftDTO)
        {
            // Don't allow wrong data
            if (id != giftDTO.Id)
            {
                _logger.LogError($"EDIT. Gift IDs do not match: giftId {giftDTO.Id}, id {id}");
                return BadRequest(new V1DTO.MessageDTO($"Could not change gift with this id: {id}"));
            }
            var gift = await _bll.Gifts.FirstOrDefaultAsync(giftDTO.Id, User.UserGuidId());
            if (gift == null)
            {
                _logger.LogError($"EDIT. No such gift: {giftDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No gift found for id {id}"));
            }
            // Allow changing own gifts only
            var personalGift = await _bll.Gifts.GetForUserAsync(id, User.UserGuidId());
            if (personalGift == null)
            {
                _logger.LogError($"EDIT. Gift {giftDTO.Id} is not owned by user {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No gift found for id {id}"));
            }
            // Update existing gift
            await _bll.Gifts.UpdateAsync(_mapper.Map(giftDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Gifts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Add new gift
        /// </summary>
        /// <param name="giftDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> PostGift(V1DTO.GiftDTO giftDTO)
        {
            // Create gift
            var bllEntity = _mapper.Map(giftDTO);
            _bll.Gifts.Add(bllEntity, User.UserGuidId());
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
        /// <summary>
        ///     Delete gift
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
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
