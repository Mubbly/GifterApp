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
    public class PrivateMessagesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<PrivateMessagesController> _logger;
        private readonly PrivateMessageMapper _mapper = new PrivateMessageMapper();

        public PrivateMessagesController(IAppBLL appBLL, ILogger<PrivateMessagesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
                // GET: api/PrivateMessages
        /// <summary>
        ///     Get all PrivateMessages
        /// </summary>
        /// <returns>List of PrivateMessages</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PrivateMessageDTO>>> GetPrivateMessages()
        {
            return Ok((await _bll.PrivateMessages.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/PrivateMessages/5
        /// <summary>
        ///     Get a single PrivateMessage
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PrivateMessage object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.PrivateMessageDTO>> GetPrivateMessage(Guid id)
        {
            var actionType = await _bll.PrivateMessages.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"PrivateMessage with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/PrivateMessages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update PrivateMessage
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
        public async Task<IActionResult> PutPrivateMessage(Guid id, V1DTO.PrivateMessageDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.PrivateMessages.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No PrivateMessage found for id {id}"));
            }
            // Update existing actionType
            // actionType.PrivateMessageValue = actionTypeEditDTO.PrivateMessageValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.PrivateMessages.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.PrivateMessages.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. PrivateMessage does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/PrivateMessages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new PrivateMessage
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PrivateMessageDTO))]
        public async Task<ActionResult<V1DTO.PrivateMessageDTO>> PostPrivateMessage(V1DTO.PrivateMessageDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.PrivateMessages.Add(bllEntity);
            
            // var actionType = new PrivateMessage
            // {
            //     PrivateMessageValue = actionTypeCreateDTO.PrivateMessageValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetPrivateMessage",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/PrivateMessages/5
        /// <summary>
        ///     Delete PrivateMessage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.PrivateMessageDTO>> DeletePrivateMessage(Guid id)
        {
            var actionType = await _bll.PrivateMessages.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"PrivateMessage with id {id} not found"));
            }
            await _bll.PrivateMessages.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }

        // // GET: api/PrivateMessages
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.PrivateMessageDTO>>> GetPrivateMessages()
        // {
        //     return await _context.PrivateMessages
        //         .Select(pm => new V1DTO.PrivateMessageDTO() 
        //         {
        //             Id = pm.Id,
        //             Message = pm.Message,
        //             IsSeen = pm.IsSeen,
        //             SentAt = pm.SentAt,
        //             UserReceiverId = pm.UserReceiverId,
        //             UserSenderId = pm.UserSenderId
        //         }).ToListAsync();
        // }
        //
        // // GET: api/PrivateMessages/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.PrivateMessageDTO>> GetPrivateMessage(Guid id)
        // {
        //     var privateMessage = await _context.PrivateMessages
        //         .Select(pm => new V1DTO.PrivateMessageDTO() 
        //         {
        //             Id = pm.Id,
        //             Message = pm.Message,
        //             IsSeen = pm.IsSeen,
        //             SentAt = pm.SentAt,
        //             UserReceiverId = pm.UserReceiverId,
        //             UserSenderId = pm.UserSenderId
        //         }).SingleOrDefaultAsync();
        //
        //     if (privateMessage == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return privateMessage;
        // }
        //
        // // PUT: api/PrivateMessages/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPrivateMessage(Guid id, PrivateMessage privateMessage)
        // {
        //     if (id != privateMessage.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(privateMessage).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PrivateMessageExists(id))
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
        // // POST: api/PrivateMessages
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<PrivateMessage>> PostPrivateMessage(PrivateMessage privateMessage)
        // {
        //     _context.PrivateMessages.Add(privateMessage);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetPrivateMessage", new { id = privateMessage.Id }, privateMessage);
        // }
        //
        // // DELETE: api/PrivateMessages/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<PrivateMessage>> DeletePrivateMessage(Guid id)
        // {
        //     var privateMessage = await _context.PrivateMessages.FindAsync(id);
        //     if (privateMessage == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.PrivateMessages.Remove(privateMessage);
        //     await _context.SaveChangesAsync();
        //
        //     return privateMessage;
        // }
        //
        // private bool PrivateMessageExists(Guid id)
        // {
        //     return _context.PrivateMessages.Any(e => e.Id == id);
        // }
    }
}
