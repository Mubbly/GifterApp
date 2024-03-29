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
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using WebApp.Helpers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ArchivedGiftsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ArchivedGiftsController> _logger;
        private readonly ArchivedGiftMapper _mapper = new ArchivedGiftMapper();

        public ArchivedGiftsController(IAppBLL appBLL, ILogger<ArchivedGiftsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/ArchivedGifts
        /// <summary>
        ///     Get all ArchivedGifts
        /// </summary>
        /// <returns>List of ArchivedGifts</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ArchivedGiftFullDTO>>> GetArchivedGifts()
        {
            return Ok((await _bll.ArchivedGifts.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ArchivedGifts/5
        /// <summary>
        ///     Get a single ArchivedGift
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ArchivedGift object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ArchivedGiftFullDTO>> GetArchivedGift(Guid id)
        {
            var actionType = await _bll.ArchivedGifts.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ArchivedGift with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/ArchivedGifts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update ArchivedGift
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionTypeFullDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutArchivedGift(Guid id, V1DTO.ArchivedGiftFullDTO actionTypeFullDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeFullDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.ArchivedGifts.FirstOrDefaultAsync(actionTypeFullDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeFullDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No ArchivedGift found for id {id}"));
            }
            // Update existing actionType
            await _bll.ArchivedGifts.UpdateAsync(_mapper.Map(actionTypeFullDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ArchivedGifts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new ArchivedGift
        /// </summary>
        /// <param name="actionTypeFullDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ArchivedGiftFullDTO))]
        public async Task<ActionResult<V1DTO.ArchivedGiftFullDTO>> PostArchivedGift(V1DTO.ArchivedGiftFullDTO actionTypeFullDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeFullDTO);
            _bll.ArchivedGifts.Add(bllEntity);
            await _bll.SaveChangesAsync();

            actionTypeFullDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetArchivedGift",
                new {id = actionTypeFullDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeFullDTO
                );
        }

        // DELETE: api/ArchivedGifts/5
        /// <summary>
        ///     Delete ArchivedGift
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ArchivedGiftFullDTO>> DeleteArchivedGift(Guid id)
        {
            var actionType = await _bll.ArchivedGifts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"ArchivedGift with id {id} not found"));
            }
            await _bll.ArchivedGifts.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }
    }
}
