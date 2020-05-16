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
    public class PermissionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<PermissionsController> _logger;
        private readonly PermissionMapper _mapper = new PermissionMapper();

        public PermissionsController(IAppBLL appBLL, ILogger<PermissionsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
                // GET: api/Permissions
        /// <summary>
        ///     Get all Permissions
        /// </summary>
        /// <returns>List of Permissions</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PermissionDTO>>> GetPermissions()
        {
            return Ok((await _bll.Permissions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Permissions/5
        /// <summary>
        ///     Get a single Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Permission object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.PermissionDTO>> GetPermission(Guid id)
        {
            var actionType = await _bll.Permissions.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Permission with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/Permissions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Permission
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
        public async Task<IActionResult> PutPermission(Guid id, V1DTO.PermissionDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.Permissions.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Permission found for id {id}"));
            }
            // Update existing actionType
            // actionType.PermissionValue = actionTypeEditDTO.PermissionValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.Permissions.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Permissions.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Permission does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Permissions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Permission
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PermissionDTO))]
        public async Task<ActionResult<V1DTO.PermissionDTO>> PostPermission(V1DTO.PermissionDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.Permissions.Add(bllEntity);
            
            // var actionType = new Permission
            // {
            //     PermissionValue = actionTypeCreateDTO.PermissionValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetPermission",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/Permissions/5
        /// <summary>
        ///     Delete Permission
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.PermissionDTO>> DeletePermission(Guid id)
        {
            var actionType = await _bll.Permissions.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Permission with id {id} not found"));
            }
            await _bll.Permissions.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }

        // // GET: api/Permissions
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.PermissionDTO>>> GetPermissions()
        // {
        //     return await _context.Permissions
        //         .Select(p => new V1DTO.PermissionDTO() 
        //         {
        //             Id = p.Id,
        //             Comment = p.Comment,
        //             PermissionValue = p.PermissionValue,
        //             UserPermissionsCount = p.UserPermissions.Count
        //         }).ToListAsync();
        // }
        //
        // // GET: api/Permissions/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.PermissionDTO>> GetPermission(Guid id)
        // {
        //     var permission = await _context.Permissions
        //         .Select(p => new V1DTO.PermissionDTO() 
        //         {
        //             Id = p.Id,
        //             Comment = p.Comment,
        //             PermissionValue = p.PermissionValue,
        //             UserPermissionsCount = p.UserPermissions.Count
        //         }).SingleOrDefaultAsync();
        //
        //     if (permission == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return permission;
        // }
        //
        // // PUT: api/Permissions/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPermission(Guid id, Permission permission)
        // {
        //     if (id != permission.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(permission).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PermissionExists(id))
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
        // // POST: api/Permissions
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Permission>> PostPermission(Permission permission)
        // {
        //     _context.Permissions.Add(permission);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetPermission", new { id = permission.Id }, permission);
        // }
        //
        // // DELETE: api/Permissions/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Permission>> DeletePermission(Guid id)
        // {
        //     var permission = await _context.Permissions.FindAsync(id);
        //     if (permission == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Permissions.Remove(permission);
        //     await _context.SaveChangesAsync();
        //
        //     return permission;
        // }
        //
        // private bool PermissionExists(Guid id)
        // {
        //     return _context.Permissions.Any(e => e.Id == id);
        // }
    }
}
