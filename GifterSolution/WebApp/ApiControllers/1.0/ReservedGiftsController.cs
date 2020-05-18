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
            // actionType.ReservedGiftValue = actionTypeEditDTO.ReservedGiftValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.ReservedGifts.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ReservedGifts.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. ReservedGift does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
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
            
            // var actionType = new ReservedGift
            // {
            //     ReservedGiftValue = actionTypeCreateDTO.ReservedGiftValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

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

        // // GET: api/ReservedGifts
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.ReservedGiftDTO>>> GetReservedGifts()
        // {
        //     return await _context.ReservedGifts
        //         .Select(rg => new V1DTO.ReservedGiftDTO() 
        //         {
        //             Id = rg.Id,
        //             Comment = rg.Comment,
        //             GiftId = rg.GiftId,
        //             ReservedFrom = rg.ReservedFrom,
        //             StatusId = rg.StatusId,
        //             ActionTypeId = rg.ActionTypeId,
        //             UserGiverId = rg.UserGiverId,
        //             UserReceiverId = rg.UserReceiverId
        //         }).ToListAsync();
        // }
        //
        // // GET: api/ReservedGifts/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.ReservedGiftDTO>> GetReservedGift(Guid id)
        // {
        //     var reservedGift = await _context.ReservedGifts
        //         .Select(rg => new V1DTO.ReservedGiftDTO() 
        //         {
        //             Id = rg.Id,
        //             Comment = rg.Comment,
        //             GiftId = rg.GiftId,
        //             ReservedFrom = rg.ReservedFrom,
        //             StatusId = rg.StatusId,
        //             ActionTypeId = rg.ActionTypeId,
        //             UserGiverId = rg.UserGiverId,
        //             UserReceiverId = rg.UserReceiverId
        //         }).SingleOrDefaultAsync();
        //
        //     if (reservedGift == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return reservedGift;
        // }
        //
        // // PUT: api/ReservedGifts/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutReservedGift(Guid id, ReservedGift reservedGift)
        // {
        //     if (id != reservedGift.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(reservedGift).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ReservedGiftExists(id))
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
        // // POST: api/ReservedGifts
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<ReservedGift>> PostReservedGift(ReservedGift reservedGift)
        // {
        //     _context.ReservedGifts.Add(reservedGift);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetReservedGift", new { id = reservedGift.Id }, reservedGift);
        // }
        //
        // // DELETE: api/ReservedGifts/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<ReservedGift>> DeleteReservedGift(Guid id)
        // {
        //     var reservedGift = await _context.ReservedGifts.FindAsync(id);
        //     if (reservedGift == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.ReservedGifts.Remove(reservedGift);
        //     await _context.SaveChangesAsync();
        //
        //     return reservedGift;
        // }
        //
        // private bool ReservedGiftExists(Guid id)
        // {
        //     return _context.ReservedGifts.Any(e => e.Id == id);
        // }
    }
}
