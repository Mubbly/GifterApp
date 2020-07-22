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
        
        // GET: api/Notifications/Personal/Active
        /// <summary>
        ///     Get all active (new/unread) personal notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet("personal/active")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.NotificationDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.UserNotificationDTO>>> GetAllPersonalActive()
        {
            var newPersonalNotifications = await _bll.Notifications.GetAllPersonalNew(User.UserGuidId());
            if (newPersonalNotifications == null)
            {
                return NotFound(new V1DTO.MessageDTO("No new notifications found"));
            }
            return Ok(newPersonalNotifications.Select(e => _mapper.MapUserNotificationBLLToDTO(e)));
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
        ///     Update Notification - mark as inactive/seen/read
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notificationEditDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutNotification(Guid id, V1DTO.UserNotificationEditDTO notificationEditDTO)
        {
            // Don't allow wrong data
            if (id != notificationEditDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and notification.id do not match"));
            }
            var userNotification = await _bll.UserNotifications.FirstOrDefaultAsync(id, User.UserGuidId());
            if (userNotification == null)
            {
                _logger.LogError($"EDIT. No such notification: {id.ToString()}, user: {User.UserGuidId().ToString()}");
                return NotFound(new V1DTO.MessageDTO($"No Notification found for id {id.ToString()}"));
            }
            // Update existing notification
            await _bll.UserNotifications.UpdateAsync(_mapper.MapUserNotificationEditToBLL(notificationEditDTO), User.UserGuidId());
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
