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
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StatusesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<StatusesController> _logger;

        public StatusesController(IAppUnitOfWork uow, ILogger<StatusesController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            // Only allow admins to interact with Statuses
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"GET. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            return Ok(await _uow.Statuses.DTOAllAsync());
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(Guid id)
        {
            // Only allow admins to interact with Statuses
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"GET/ID. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }

            var statusDTO = await _uow.Statuses.DTOFirstOrDefaultAsync(id);
            if (statusDTO == null)
            {
                return NotFound();
            }

            return Ok(statusDTO);
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, StatusEditDTO statusEditDTO)
        {
            // Only allow admins to interact with Statuses
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"EDIT. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            
            // Don't allow wrong data
            if (id != statusEditDTO.Id)
            {
                return BadRequest();
            }
            var status = await _uow.Statuses.FirstOrDefaultAsync(statusEditDTO.Id, User.UserGuidId());
            if (status == null)
            {
                _logger.LogError($"EDIT. No such status: {statusEditDTO.Id}, user: {User.UserGuidId()}");
                return BadRequest();
            }
            
            // Update existing status
            status.StatusValue = statusEditDTO.StatusValue;
            status.Comment = statusEditDTO.Comment;

            _uow.Statuses.Update(status);
            
            // Save to db
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Statuses.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError($"EDIT. Status does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // POST: api/Statuses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(StatusCreateDTO statusCreateDTO)
        {
            // Only allow admins to interact with Statuses
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"CREATE. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            
            _logger.LogError($"{statusCreateDTO.Comment}");

            // Create status
            var status = new Status
            {
                StatusValue = statusCreateDTO.StatusValue,
                Comment = statusCreateDTO.Comment
            };
            _uow.Statuses.Add(status);
            
            await _uow.SaveChangesAsync();
            
            statusCreateDTO.Id = status.Id;
            return CreatedAtAction("GetStatus", new { id = status.Id }, statusCreateDTO);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Status>> DeleteStatus(Guid id)
        {
            // Only allow admins to interact with Statuses
            if (!User.IsInRole("Admin"))
            {
                _logger.LogError($"DELETE. This user is not an admin: {User.UserGuidId()}");
                return StatusCode(403);
            }
            var status = await _uow.Statuses.FirstOrDefaultAsync(id, User.UserGuidId());
            if (status == null)
            {
                _logger.LogError($"DELETE. No such status: {id}, user: {User.UserGuidId()}");
                return NotFound();
            }
            _uow.Statuses.Remove(status);
            
            await _uow.SaveChangesAsync();
            return Ok(status);
        }
    }
}
