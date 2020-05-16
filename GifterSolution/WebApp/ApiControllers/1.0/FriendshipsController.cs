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
    public class FriendshipsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<FriendshipsController> _logger;
        private readonly FriendshipMapper _mapper = new FriendshipMapper();

        public FriendshipsController(IAppBLL appBLL, ILogger<FriendshipsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
                // GET: api/Friendships
        /// <summary>
        ///     Get all Friendships
        /// </summary>
        /// <returns>List of Friendships</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetFriendships()
        {
            return Ok((await _bll.Friendships.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Friendships/5
        /// <summary>
        ///     Get a single Friendship
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Friendship object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> GetFriendship(Guid id)
        {
            var actionType = await _bll.Friendships.FirstOrDefaultAsync(id);
            if (actionType == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Friendship with id {id} not found"));
            }

            return Ok(_mapper.Map(actionType));
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Friendship
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
        public async Task<IActionResult> PutFriendship(Guid id, V1DTO.FriendshipDTO actionTypeDTO)
        {
            // Don't allow wrong data
            if (id != actionTypeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
            }
            var actionType = await _bll.Friendships.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Friendship found for id {id}"));
            }
            // Update existing actionType
            // actionType.FriendshipValue = actionTypeEditDTO.FriendshipValue;
            // actionType.Comment = actionTypeEditDTO.Comment;
            await _bll.Friendships.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Friendships.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Friendship does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Friendships
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Friendship
        /// </summary>
        /// <param name="actionTypeDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.FriendshipDTO))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> PostFriendship(V1DTO.FriendshipDTO actionTypeDTO)
        {
            // Create actionType
            var bllEntity = _mapper.Map(actionTypeDTO);
            _bll.Friendships.Add(bllEntity);
            
            // var actionType = new Friendship
            // {
            //     FriendshipValue = actionTypeCreateDTO.FriendshipValue,
            //     Comment = actionTypeCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            actionTypeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetFriendship",
                new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                actionTypeDTO
                );
        }

        // DELETE: api/Friendships/5
        /// <summary>
        ///     Delete Friendship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.FriendshipDTO>> DeleteFriendship(Guid id)
        {
            var actionType = await _bll.Friendships.FirstOrDefaultAsync(id, User.UserGuidId());
            if (actionType == null)
            {
                _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Friendship with id {id} not found"));
            }
            await _bll.Friendships.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(actionType);
        }

        // // GET: api/Friendships
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.FriendshipDTO>>> GetFriendships()
        // {
        //     return await _context.Friendships
        //         .Select(f => new V1DTO.FriendshipDTO()
        //         {
        //             Id = f.Id,
        //             Comment = f.Comment,
        //             AppUser1Id = f.AppUser1Id,
        //             AppUser2Id = f.AppUser2Id,
        //             IsConfirmed = f.IsConfirmed
        //         }).ToListAsync();
        // }
        //
        // // GET: api/Friendships/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.FriendshipDTO>> GetFriendship(Guid id)
        // {
        //     var friendship = await _context.Friendships
        //         .Select(f => new V1DTO.FriendshipDTO()
        //         {
        //             Id = f.Id,
        //             Comment = f.Comment,
        //             AppUser1Id = f.AppUser1Id,
        //             AppUser2Id = f.AppUser2Id,
        //             IsConfirmed = f.IsConfirmed
        //         }).SingleOrDefaultAsync();
        //
        //     if (friendship == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return friendship;
        // }
        //
        // // PUT: api/Friendships/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutFriendship(Guid id, Friendship friendship)
        // {
        //     if (id != friendship.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(friendship).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!FriendshipExists(id))
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
        // // POST: api/Friendships
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        // {
        //     _context.Friendships.Add(friendship);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetFriendship", new { id = friendship.Id }, friendship);
        // }
        //
        // // DELETE: api/Friendships/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Friendship>> DeleteFriendship(Guid id)
        // {
        //     var friendship = await _context.Friendships.FindAsync(id);
        //     if (friendship == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Friendships.Remove(friendship);
        //     await _context.SaveChangesAsync();
        //
        //     return friendship;
        // }
        //
        // private bool FriendshipExists(Guid id)
        // {
        //     return _context.Friendships.Any(e => e.Id == id);
        // }
    }
}
