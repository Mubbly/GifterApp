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
            var permission = await _bll.Permissions.FirstOrDefaultAsync(id);
            if (permission == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Permission with id {id} not found"));
            }
            return Ok(_mapper.Map(permission));
        }

        // PUT: api/Permissions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Permission
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutPermission(Guid id, V1DTO.PermissionDTO permissionDTO)
        {
            // Don't allow wrong data
            if (id != permissionDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and permission.id do not match"));
            }
            var permission = await _bll.Permissions.FirstOrDefaultAsync(permissionDTO.Id, User.UserGuidId());
            if (permission == null)
            {
                _logger.LogError($"EDIT. No such permission: {permissionDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Permission found for id {id}"));
            }
            // Update existing permission
            await _bll.Permissions.UpdateAsync(_mapper.Map(permissionDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Permissions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Permission
        /// </summary>
        /// <param name="permissionDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PermissionDTO))]
        public async Task<ActionResult<V1DTO.PermissionDTO>> PostPermission(V1DTO.PermissionDTO permissionDTO)
        {
            // Create permission
            var bllEntity = _mapper.Map(permissionDTO);
            _bll.Permissions.Add(bllEntity);
            await _bll.SaveChangesAsync();

            permissionDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetPermission",
                new {id = permissionDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                permissionDTO
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
            var permission = await _bll.Permissions.FirstOrDefaultAsync(id, User.UserGuidId());
            if (permission == null)
            {
                _logger.LogError($"DELETE. No such permission: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Permission with id {id} not found"));
            }
            await _bll.Permissions.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(permission);
        }
    }
}
