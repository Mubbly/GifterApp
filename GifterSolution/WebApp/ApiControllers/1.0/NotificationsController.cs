using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
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
            var actionType = await _bll.Notifications.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Notification with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Notification
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
        public async Task<IActionResult> PutNotification(Guid id, V1DTO.NotificationDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.Notifications.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Notification found for id {id}"));
            }
            // Update existing actionType
            // actionType.NotificationValue = actionTypeEditDTO.NotificationValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.Notifications.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Notifications.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Notification does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Notifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Notification
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.NotificationDTO))]
        public async Task<ActionResult<V1DTO.NotificationDTO>> PostNotification(V1DTO.NotificationDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.Notifications.Add(bllEntity);
            
            // var actionType = new Notification
            // {
            //     NotificationValue = actionTypeCreateDTO.NotificationValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetNotification",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
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
            var actionType = await _bll.Notifications.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Notification with id {id} not found"));
            }
            await _bll.Notifications.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }

        // // GET: api/Notifications
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.NotificationDTO>>> GetNotifications()
        // {
        //     return await _context.Notifications
        //         .Select(n => new V1DTO.NotificationDTO() 
        //         {
        //             Id = n.Id,
        //             Comment = n.Comment,
        //             NotificationTypeId = n.NotificationTypeId,
        //             NotificationValue = n.NotificationValue,
        //             UserNotificationsCount = n.UserNotifications.Count
        //         }).ToListAsync();
        // }
        //
        // // GET: api/Notifications/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.NotificationDTO>> GetNotification(Guid id)
        // {
        //     var notification = await _context.Notifications
        //         .Select(n => new V1DTO.NotificationDTO() 
        //         {
        //             Id = n.Id,
        //             Comment = n.Comment,
        //             NotificationTypeId = n.NotificationTypeId,
        //             NotificationValue = n.NotificationValue,
        //             UserNotificationsCount = n.UserNotifications.Count
        //         }).SingleOrDefaultAsync();
        //
        //     if (notification == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return notification;
        // }
        //
        // // PUT: api/Notifications/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutNotification(Guid id, Notification notification)
        // {
        //     if (id != notification.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(notification).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!NotificationExists(id))
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
        // // POST: api/Notifications
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        // {
        //     _context.Notifications.Add(notification);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        // }
        //
        // // DELETE: api/Notifications/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Notification>> DeleteNotification(Guid id)
        // {
        //     var notification = await _context.Notifications.FindAsync(id);
        //     if (notification == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Notifications.Remove(notification);
        //     await _context.SaveChangesAsync();
        //
        //     return notification;
        // }
        //
        // private bool NotificationExists(Guid id)
        // {
        //     return _context.Notifications.Any(e => e.Id == id);
        // }
    }
}
