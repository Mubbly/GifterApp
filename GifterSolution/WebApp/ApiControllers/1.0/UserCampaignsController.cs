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
    public class UserCampaignsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<UserCampaignsController> _logger;
        private readonly UserCampaignMapper _mapper = new UserCampaignMapper();

        public UserCampaignsController(IAppBLL appBLL, ILogger<UserCampaignsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/UserCampaigns
        /// <summary>
        ///     Get all UserCampaigns
        /// </summary>
        /// <returns>List of UserCampaigns</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.UserCampaignDTO>>> GetUserCampaigns()
        {
            return Ok((await _bll.UserCampaigns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/UserCampaigns/5
        /// <summary>
        ///     Get a single UserCampaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserCampaign object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.UserCampaignDTO>> GetUserCampaign(Guid id)
        {
            var userCampaign = await _bll.UserCampaigns.FirstOrDefaultAsync(id);
            if (userCampaign == null)
            {
                return NotFound(new V1DTO.MessageDTO($"UserCampaign with id {id} not found"));
            }

            return Ok(_mapper.Map(userCampaign));
        }

        // PUT: api/UserCampaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update UserCampaign
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userCampaignDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUserCampaign(Guid id, V1DTO.UserCampaignDTO userCampaignDTO)
        {
            // Don't allow wrong data
            if (id != userCampaignDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and userCampaign.id do not match"));
            }
            var userCampaign = await _bll.UserCampaigns.FirstOrDefaultAsync(userCampaignDTO.Id, User.UserGuidId());
            if (userCampaign == null)
            {
                _logger.LogError($"EDIT. No such userCampaign: {userCampaignDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No UserCampaign found for id {id}"));
            }
            // Update existing userCampaign
            // userCampaign.UserCampaignValue = userCampaignEditDTO.UserCampaignValue;
            // userCampaign.Comment = userCampaignEditDTO.Comment;
            await _bll.UserCampaigns.UpdateAsync(_mapper.Map(userCampaignDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.UserCampaigns.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. UserCampaign does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/UserCampaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new UserCampaign
        /// </summary>
        /// <param name="userCampaignDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.UserCampaignDTO))]
        public async Task<ActionResult<V1DTO.UserCampaignDTO>> PostUserCampaign(V1DTO.UserCampaignDTO userCampaignDTO)
        {
            // Create userCampaign
            var bllEntity = _mapper.Map(userCampaignDTO);
            _bll.UserCampaigns.Add(bllEntity);
            
            // var userCampaign = new UserCampaign
            // {
            //     UserCampaignValue = userCampaignCreateDTO.UserCampaignValue,
            //     Comment = userCampaignCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            userCampaignDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetUserCampaign",
                new {id = userCampaignDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                userCampaignDTO
                );
        }

        // DELETE: api/UserCampaigns/5
        /// <summary>
        ///     Delete UserCampaign
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.UserCampaignDTO>> DeleteUserCampaign(Guid id)
        {
            var userCampaign = await _bll.UserCampaigns.FirstOrDefaultAsync(id, User.UserGuidId());
            if (userCampaign == null)
            {
                _logger.LogError($"DELETE. No such userCampaign: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"UserCampaign with id {id} not found"));
            }
            await _bll.UserCampaigns.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(userCampaign);
        }

        // // GET: api/UserCampaigns
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.UserCampaignDTO>>> GetUserCampaigns()
        // {
        //     return await _context.UserCampaigns
        //         .Select(uc => new V1DTO.UserCampaignDTO() 
        //         {
        //             Id = uc.Id,
        //             Comment = uc.Comment,
        //             CampaignId = uc.CampaignId,
        //             AppUserId = uc.AppUserId
        //         }).ToListAsync();
        // }
        //
        // // GET: api/UserCampaigns/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.UserCampaignDTO>> GetUserCampaign(Guid id)
        // {
        //     var userCampaign = await _context.UserCampaigns
        //         .Select(uc => new V1DTO.UserCampaignDTO() 
        //         {
        //             Id = uc.Id,
        //             Comment = uc.Comment,
        //             CampaignId = uc.CampaignId,
        //             AppUserId = uc.AppUserId
        //         }).SingleOrDefaultAsync();
        //
        //     if (userCampaign == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return userCampaign;
        // }
        //
        // // PUT: api/UserCampaigns/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUserCampaign(Guid id, UserCampaign userCampaign)
        // {
        //     if (id != userCampaign.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(userCampaign).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserCampaignExists(id))
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
        // // POST: api/UserCampaigns
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<UserCampaign>> PostUserCampaign(UserCampaign userCampaign)
        // {
        //     _context.UserCampaigns.Add(userCampaign);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetUserCampaign", new { id = userCampaign.Id }, userCampaign);
        // }
        //
        // // DELETE: api/UserCampaigns/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<UserCampaign>> DeleteUserCampaign(Guid id)
        // {
        //     var userCampaign = await _context.UserCampaigns.FindAsync(id);
        //     if (userCampaign == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.UserCampaigns.Remove(userCampaign);
        //     await _context.SaveChangesAsync();
        //
        //     return userCampaign;
        // }
        //
        // private bool UserCampaignExists(Guid id)
        // {
        //     return _context.UserCampaigns.Any(e => e.Id == id);
        // }
    }
}
