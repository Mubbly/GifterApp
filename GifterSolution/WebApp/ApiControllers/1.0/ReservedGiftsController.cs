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
    public class ReservedGiftsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ReservedGiftsController> _logger;
        private readonly ReservedGiftMapper _mapper = new ReservedGiftMapper();

        public ReservedGiftsController(IAppBLL appBLL, ILogger<ReservedGiftsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
                // GET: api/ReservedGifts
        /// <summary>
        ///     Get all ReservedGifts
        /// </summary>
        /// <returns>List of ReservedGifts</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ReservedGiftDTO>>> GetReservedGifts()
        {
            return Ok((await _bll.ReservedGifts.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ReservedGifts/5
        /// <summary>
        ///     Get a single ReservedGift
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ReservedGift object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ReservedGiftDTO>> GetReservedGift(Guid id)
        {
            var actionType = await _bll.ReservedGifts.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {id} not found"));
            }
            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/ReservedGifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update ReservedGift
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutReservedGift(Guid id, V1DTO.ReservedGiftDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.ReservedGifts.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No ReservedGift found for id {id}"));
            }
            // Update existing actionType
            await _bll.ReservedGifts.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ReservedGifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new ReservedGift
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ReservedGiftDTO))]
        public async Task<ActionResult<V1DTO.ReservedGiftDTO>> PostReservedGift(V1DTO.ReservedGiftDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.ReservedGifts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetReservedGift",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/ReservedGifts/5
        /// <summary>
        ///     Delete ReservedGift
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ReservedGiftDTO>> DeleteReservedGift(Guid id)
        {
            var actionType = await _bll.ReservedGifts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {id} not found"));
            }
            await _bll.ReservedGifts.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }
    }
}
