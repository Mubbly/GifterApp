using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class ActionTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<ActionTypesController> _logger;

        public ActionTypesController(IAppUnitOfWork uow, ILogger<ActionTypesController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET: api/ActionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionTypeDTO>>> GetActionTypes()
        {
            // Only allow admins to interact with ActionTypes
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"GET. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            return Ok(await _uow.ActionTypes.DTOAllAsync());
        }

        // GET: api/ActionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActionTypeDTO>> GetActionType(Guid id)
        {
            // Only allow admins to interact with ActionTypes
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"GET/ID. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }

            var actionTypeDTO = await _uow.ActionTypes.DTOFirstOrDefaultAsync(id);
            if (actionTypeDTO == null)
            {
                return NotFound();
            }

            return Ok(actionTypeDTO);
        }

        // PUT: api/ActionTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActionType(Guid id, ActionTypeEditDTO actionTypeEditDTO)
        {
            // Only allow admins to interact with ActionTypes
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"EDIT. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            
            // Don't allow wrong data
            if (id != actionTypeEditDTO.Id)
            {
                return BadRequest();
            }
            var actionType = await _uow.ActionTypes.FirstOrDefaultAsync(actionTypeEditDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeEditDTO.Id}, user: {User.UserGuidId()}");
                return BadRequest();
            }
            
            // Update existing actionType
            actionType.ActionTypeValue = actionTypeEditDTO.ActionTypeValue;
            actionType.Comment = actionTypeEditDTO.Comment;

            _uow.ActionTypes.Update(actionType);
            
            // Save to db
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.ActionTypes.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError($"EDIT. ActionType does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // POST: api/ActionTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ActionTypeCreateDTO>> PostActionType(ActionTypeCreateDTO actionTypeCreateDTO)
        {
            // Only allow admins to interact with ActionTypes
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"CREATE. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            
            _logger.LogError($"{actionTypeCreateDTO.Comment}");

            // Create actionType
            var actionType = new ActionType
            {
                ActionTypeValue = actionTypeCreateDTO.ActionTypeValue,
                Comment = actionTypeCreateDTO.Comment
            };
            _uow.ActionTypes.Add(actionType);
            
            await _uow.SaveChangesAsync();
            
            actionTypeCreateDTO.Id = actionType.Id;
            return CreatedAtAction("GetActionType", new { id = actionType.Id }, actionTypeCreateDTO);
        }

        // DELETE: api/ActionTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActionType>> DeleteActionType(Guid id)
        {
            // Only allow admins to interact with ActionTypes
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"DELETE. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            var actionType = await _uow.ActionTypes.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound();
            }
            _uow.ActionTypes.Remove(actionType);
            
            await _uow.SaveChangesAsync();
            return Ok(actionType);
        }
    }
}
