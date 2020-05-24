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
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")] // Only allow for admins
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class NotificationTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<NotificationTypesController> _logger;
        private readonly NotificationTypeMapper _mapper = new NotificationTypeMapper();

        public NotificationTypesController(IAppBLL appBLL, ILogger<NotificationTypesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/NotificationTypes
        /// <summary>
        ///     Get all NotificationTypes
        /// </summary>
        /// <returns>List of NotificationTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.NotificationTypeDTO>>> GetNotificationTypes()
        {
            return Ok((await _bll.NotificationTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/NotificationTypes/5
        /// <summary>
        ///     Get a single NotificationType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NotificationType object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.NotificationTypeDTO>> GetNotificationType(Guid id)
        {
            var actionType = await _bll.NotificationTypes.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"NotificationType with id {id} not found"));
            }
            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/NotificationTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update NotificationType
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
        public async Task<IActionResult> PutNotificationType(Guid id, V1DTO.NotificationTypeDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.NotificationTypes.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No NotificationType found for id {id}"));
            }
            // Update existing actionType
            await _bll.NotificationTypes.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/NotificationTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new NotificationType
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.NotificationTypeDTO))]
        public async Task<ActionResult<V1DTO.NotificationTypeDTO>> PostNotificationType(V1DTO.NotificationTypeDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.NotificationTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetNotificationType",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/NotificationTypes/5
        /// <summary>
        ///     Delete NotificationType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.NotificationTypeDTO>> DeleteNotificationType(Guid id)
        {
            var actionType = await _bll.NotificationTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"NotificationType with id {id} not found"));
            }
            await _bll.NotificationTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }
    }
}
