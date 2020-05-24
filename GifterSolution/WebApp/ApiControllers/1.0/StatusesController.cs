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
    public class StatusesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<StatusesController> _logger;
        private readonly StatusMapper _mapper = new StatusMapper();

        public StatusesController(IAppBLL appBLL, ILogger<StatusesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Statuses
        /// <summary>
        ///     Get all Statuses
        /// </summary>
        /// <returns>List of Statuses</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.StatusDTO>>> GetStatuses()
        {
            return Ok((await _bll.Statuses.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Statuses/5
        /// <summary>
        ///     Get a single Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.StatusDTO>> GetStatus(Guid id)
        {
            var status = await _bll.Statuses.FirstOrDefaultAsync(id);
            if (status == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Status with id {id} not found"));
            }
            return Ok(_mapper.Map(status));
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutStatus(Guid id, V1DTO.StatusDTO statusDTO)
        {
            // Don't allow wrong data
            if (id != statusDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and status.id do not match"));
            }
            var status = await _bll.Statuses.FirstOrDefaultAsync(statusDTO.Id, User.UserGuidId());
            if (status == null)
            {
                _logger.LogError($"EDIT. No such status: {statusDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Status found for id {id}"));
            }
            // Update existing status
            await _bll.Statuses.UpdateAsync(_mapper.Map(statusDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Statuses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Status
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.StatusDTO))]
        public async Task<ActionResult<V1DTO.StatusDTO>> PostStatus(V1DTO.StatusDTO statusDTO)
        {
            // Create status
            var bllEntity = _mapper.Map(statusDTO);
            _bll.Statuses.Add(bllEntity);
            await _bll.SaveChangesAsync();

            statusDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetStatus",
                new {id = statusDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                statusDTO
                );
        }

        // DELETE: api/Statuses/5
        /// <summary>
        ///     Delete Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.StatusDTO>> DeleteStatus(Guid id)
        {
            var status = await _bll.Statuses.FirstOrDefaultAsync(id, User.UserGuidId());
            if (status == null)
            {
                _logger.LogError($"DELETE. No such status: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Status with id {id} not found"));
            }
            await _bll.Statuses.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(status);
        }
    }
}
