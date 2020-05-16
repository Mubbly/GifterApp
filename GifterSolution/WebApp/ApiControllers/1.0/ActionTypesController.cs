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
    /// <summary>
    ///     Action types are specific interactions that can be taken with something
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/admin/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ActionTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ActionTypesController> _logger;
        private readonly ActionTypeMapper _mapper = new ActionTypeMapper();

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="appBLL"></param>
        /// <param name="logger"></param>
        public ActionTypesController(IAppBLL appBLL, ILogger<ActionTypesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }

        // GET: api/ActionTypes
        /// <summary>
        ///     Get all ActionTypes
        /// </summary>
        /// <returns>List of ActionTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ActionTypeDTO>>> GetActionTypes()
        {
            return Ok((await _bll.ActionTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ActionTypes/5
        /// <summary>
        ///     Get a single ActionType
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionType object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ActionTypeDTO>> GetActionType(Guid id)
        {
            var actionType = await _bll.ActionTypes.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ActionType with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/ActionTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update ActionType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutActionType(Guid id, V1DTO.ActionTypeDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.ActionTypes.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No ActionType found for id {id}"));
            }
            // Update existing actionType
            // actionType.ActionTypeValue = actionTypeEditDTO.ActionTypeValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.ActionTypes.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.ActionTypes.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. ActionType does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/ActionTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new ActionType
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.ActionTypeDTO>> PostActionType(V1DTO.ActionTypeDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.ActionTypes.Add(bllEntity);
            
            // var actionType = new ActionType
            // {
            //     ActionTypeValue = actionTypeCreateDTO.ActionTypeValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetActionType",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/ActionTypes/5
        /// <summary>
        ///     Delete ActionType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.ActionTypeDTO>> DeleteActionType(Guid id)
        {
            var actionType = await _bll.ActionTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"ActionType with id {id} not found"));
            }
            await _bll.ActionTypes.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }
    }
}