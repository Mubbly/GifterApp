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
    public class NotificationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<NotificationsController> _logger;
        private readonly NotificationMapper _mapper = new NotificationMapper();

        public NotificationsController(IAppBLL appBLL, ILogger<NotificationsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
                // GET: api/Notifications
        /// <summary>
        ///     Get all Notifications
        /// </summary>
        /// <returns>List of Notifications</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.NotificationDTO>>> GetNotifications()
        {
            return Ok((await _bll.Notifications.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Notifications/5
        /// <summary>
        ///     Get a single Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Notification object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.NotificationDTO>> GetNotification(Guid id)
        {
            var notification = await _bll.Notifications.FirstOrDefaultAsync(id);
            if (notification == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Notification with id {id} not found"));
            }
            return Ok(_mapper.Map(notification));
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Notification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notificationDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutNotification(Guid id, V1DTO.NotificationDTO notificationDTO)
        {
            // Don't allow wrong data
            if (id != notificationDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and notification.id do not match"));
            }
            var notification = await _bll.Notifications.FirstOrDefaultAsync(notificationDTO.Id, User.UserGuidId());
            if (notification == null)
            {
                _logger.LogError($"EDIT. No such notification: {notificationDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Notification found for id {id}"));
            }
            // Update existing notification
            await _bll.Notifications.UpdateAsync(_mapper.Map(notificationDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Notifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Notification
        /// </summary>
        /// <param name="notificationDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.NotificationDTO))]
        public async Task<ActionResult<V1DTO.NotificationDTO>> PostNotification(V1DTO.NotificationDTO notificationDTO)
        {
            // Create notification
            var bllEntity = _mapper.Map(notificationDTO);
            _bll.Notifications.Add(bllEntity);
            await _bll.SaveChangesAsync();

            notificationDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetNotification",
                new {id = notificationDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                notificationDTO
                );
        }

        // DELETE: api/Notifications/5
        /// <summary>
        ///     Delete Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.NotificationDTO>> DeleteNotification(Guid id)
        {
            var notification = await _bll.Notifications.FirstOrDefaultAsync(id, User.UserGuidId());
            if (notification == null)
            {
                _logger.LogError($"DELETE. No such notification: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Notification with id {id} not found"));
            }
            await _bll.Notifications.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(notification);
        }
    }
}
