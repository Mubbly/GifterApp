using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    public class DonateesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<DonateesController> _logger;
        private readonly DonateeMapper _mapper = new DonateeMapper();

        public DonateesController(IAppBLL appBLL, ILogger<DonateesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Donatees
        /// <summary>
        ///     Get all Donatees
        /// </summary>
        /// <returns>List of Donatees</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.DonateeDTO>>> GetDonatees()
        {
            return Ok((await _bll.Donatees.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Donatees/5
        /// <summary>
        ///     Get a single Donatee
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Donatee object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.DonateeDTO>> GetDonatee(Guid id)
        {
            var donatee = await _bll.Donatees.FirstOrDefaultAsync(id);
            if (donatee == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Donatee with id {id} not found"));
            }

            return Ok(_mapper.Map(donatee));
        }
        
        // GET: api/Donatees/Campaign/5
        /// <summary>
        ///     Get all Donatees related to a Campaign
        /// </summary>
        /// <returns>List of Donatees for certain Campaign</returns>
        [HttpGet("campaign/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.DonateeDTO>>> GetDonateesForCampaign(Guid campaignId)
        {
            var campaignDonatees = await _bll.Donatees.GetAllForCampaignAsync(campaignId, User.UserGuidId());
            return Ok(campaignDonatees.Select(e => _mapper.Map(e)));
        }

        // PUT: api/Donatees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Donatee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="donateeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDonatee(Guid id, V1DTO.DonateeDTO donateeDTO)
        {
            // Don't allow wrong data
            if (id != donateeDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and donatee.id do not match"));
            }
            var donatee = await _bll.Donatees.FirstOrDefaultAsync(donateeDTO.Id, User.UserGuidId());
            if (donatee == null)
            {
                _logger.LogError($"EDIT. No such donatee: {donateeDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Donatee found for id {id}"));
            }
            // Update existing donatee
            // donatee.DonateeValue = donateeEditDTO.DonateeValue;
            // donatee.Comment = donateeEditDTO.Comment;
            await _bll.Donatees.UpdateAsync(_mapper.Map(donateeDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Donatees.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Donatee does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Donatees/Campaign/6
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Donatee
        /// </summary>
        /// <param name="donateeDTO"></param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        [HttpPost("Campaign/{campaignId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.DonateeDTO))]
        public async Task<ActionResult<V1DTO.DonateeDTO>> PostDonatee(Guid campaignId, V1DTO.DonateeDTO donateeDTO)
        {
            // Create donatee
            var bllEntity = _mapper.Map(donateeDTO);
            _bll.Donatees.Add(bllEntity, campaignId, User.UserGuidId());

            await _bll.SaveChangesAsync();

            donateeDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetDonatee",
                new {id = donateeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                donateeDTO
                );
        }

        // DELETE: api/Donatees/5
        /// <summary>
        ///     Delete Donatee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.DonateeDTO>> DeleteDonatee(Guid id)
        {
            var donatee = await _bll.Donatees.FirstOrDefaultAsync(id, User.UserGuidId());
            if (donatee == null)
            {
                _logger.LogError($"DELETE. No such donatee: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Donatee with id {id} not found"));
            }
            await _bll.Donatees.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(donatee);
        }

        // // GET: api/Donatees
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.DonateeDTO>>> GetDonatees()
        // {
        //     return Ok(await _uow.Donatees.DTOAllAsync());
        // }
        //
        // // GET: api/Donatees/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.DonateeDTO>> GetDonatee(Guid id)
        // {
        //     var donatee = await _uow.Donatees.DTOFirstOrDefaultAsync(id);
        //
        //     if (donatee == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return donatee;
        // }
        //
        // // PUT: api/Donatees/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutDonatee(Guid id, DonateeEditDTO donateeEditDTO)
        // {
        //     if (id != donateeEditDTO.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     var donatee = await _uow.Donatees.FirstOrDefaultAsync(donateeEditDTO.Id);
        //     if (donatee == null)
        //     {
        //         return BadRequest();
        //     }
        //     donatee.FirstName = donateeEditDTO.FirstName;
        //     donatee.LastName = donateeEditDTO.LastName;
        //     donatee.Age = donateeEditDTO.Age;
        //     donatee.Gender = donateeEditDTO.Gender;
        //     donatee.Bio = donateeEditDTO.Bio;
        //     donatee.GiftName = donateeEditDTO.GiftName;
        //     donatee.GiftDescription = donateeEditDTO.GiftDescription;
        //     donatee.GiftImage = donateeEditDTO.GiftImage;
        //     donatee.GiftUrl = donateeEditDTO.GiftUrl;
        //     donatee.ActiveFrom = donateeEditDTO.ActiveFrom;
        //     donatee.ActiveTo = donateeEditDTO.ActiveTo;
        //     donatee.ActionTypeId = donateeEditDTO.ActionTypeId;
        //     donatee.StatusId = donateeEditDTO.StatusId;
        //
        //     _uow.Donatees.Update(donatee);
        //     
        //     try
        //     {
        //         await _uow.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!await _uow.Donatees.ExistsAsync(id))
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
        // // POST: api/Donatees
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Donatee>> PostDonatee(DonateeCreateDTO donateeCreateDTO)
        // {
        //     var donatee = new Donatee
        //     {
        //         Id = donateeCreateDTO.Id,
        //         FirstName = donateeCreateDTO.FirstName,
        //         LastName = donateeCreateDTO.LastName,
        //         Age = donateeCreateDTO.Age,
        //         Gender = donateeCreateDTO.Gender,
        //         Bio = donateeCreateDTO.Bio,
        //         GiftName = donateeCreateDTO.GiftName,
        //         GiftDescription = donateeCreateDTO.GiftDescription,
        //         GiftImage = donateeCreateDTO.GiftImage,
        //         GiftUrl = donateeCreateDTO.GiftUrl,
        //         ActiveFrom = donateeCreateDTO.ActiveFrom,
        //         ActiveTo = donateeCreateDTO.ActiveTo,
        //         ActionTypeId = donateeCreateDTO.ActionTypeId,
        //         StatusId = donateeCreateDTO.StatusId
        //     };
        //     
        //     _uow.Donatees.Add(donatee);
        //     
        //     await _uow.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetDonatee", new { id = donatee.Id }, donatee);
        // }
        //
        // // DELETE: api/Donatees/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Donatee>> DeleteDonatee(Guid id)
        // {
        //     var donatee = await _uow.Donatees.FirstOrDefaultAsync(id);
        //     if (donatee == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _uow.Donatees.Remove(donatee);
        //     await _uow.SaveChangesAsync();
        //
        //     return Ok(donatee);
        // }
    }
}
