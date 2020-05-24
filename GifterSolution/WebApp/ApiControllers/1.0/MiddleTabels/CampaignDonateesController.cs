// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Contracts.BLL.App;
// using Extensions;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using PublicApi.DTO.v1.Mappers;
// using V1DTO = PublicApi.DTO.v1;
//
// namespace WebApp.ApiControllers._1._0
// {
//     [ApiController]
//     [ApiVersion("1.0")]
//     [Route("api/v{version:apiVersion}/[controller]")]
//     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//     [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//     public class CampaignDonateesController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly ILogger<CampaignDonateesController> _logger;
//         private readonly CampaignDonateeMapper _mapper = new CampaignDonateeMapper();
//
//         public CampaignDonateesController(IAppBLL appBLL, ILogger<CampaignDonateesController> logger)
//         {
//             _bll = appBLL;
//             _logger = logger;
//         }
//         
//                 // GET: api/CampaignDonatees
//         /// <summary>
//         ///     Get all CampaignDonatees
//         /// </summary>
//         /// <returns>List of CampaignDonatees</returns>
//         [HttpGet]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<IEnumerable<V1DTO.CampaignDonateeDTO>>> GetCampaignDonatees()
//         {
//             return Ok((await _bll.CampaignDonatees.GetAllAsync()).Select(e => _mapper.Map(e)));
//         }
//
//         // GET: api/CampaignDonatees/5
//         /// <summary>
//         ///     Get a single CampaignDonatee
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns>CampaignDonatee object</returns>
//         [HttpGet("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.CampaignDonateeDTO>> GetCampaignDonatee(Guid id)
//         {
//             var actionType = await _bll.CampaignDonatees.FirstOrDefaultAsync(id);
//             if (actionType == null)
//             {
//                 return NotFound(new V1DTO.MessageDTO($"CampaignDonatee with id {id} not found"));
//             }
//
//             return Ok(_mapper.Map(actionType));
//         }
//
//         // PUT: api/CampaignDonatees/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Update CampaignDonatee
//         /// </summary>
//         /// <param name="id"></param>
//         /// <param name="actionTypeDTO"></param>
//         /// <returns></returns>
//         [HttpPut("{id}")]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         public async Task<IActionResult> PutCampaignDonatee(Guid id, V1DTO.CampaignDonateeDTO actionTypeDTO)
//         {
//             // Don't allow wrong data
//             if (id != actionTypeDTO.Id)
//             {
//                 return BadRequest(new V1DTO.MessageDTO("id and actionType.id do not match"));
//             }
//             var actionType = await _bll.CampaignDonatees.FirstOrDefaultAsync(actionTypeDTO.Id, User.UserGuidId());
//             if (actionType == null)
//             {
//                 _logger.LogError($"EDIT. No such actionType: {actionTypeDTO.Id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"No CampaignDonatee found for id {id}"));
//             }
//             // Update existing actionType
//             // actionType.CampaignDonateeValue = actionTypeEditDTO.CampaignDonateeValue;
//             // actionType.Comment = actionTypeEditDTO.Comment;
//             await _bll.CampaignDonatees.UpdateAsync(_mapper.Map(actionTypeDTO), User.UserId());
//
//             // Save to db
//             try
//             {
//                 await _bll.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!await _bll.CampaignDonatees.ExistsAsync(id, User.UserGuidId()))
//                 {
//                     _logger.LogError(
//                         $"EDIT. CampaignDonatee does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
//                     return NotFound();
//                 }
//
//                 throw;
//             }
//             return NoContent();
//         }
//
//         // POST: api/CampaignDonatees
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Add new CampaignDonatee
//         /// </summary>
//         /// <param name="actionTypeDTO"></param>
//         /// <returns></returns>
//         [HttpPost]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.CampaignDonateeDTO))]
//         public async Task<ActionResult<V1DTO.CampaignDonateeDTO>> PostCampaignDonatee(V1DTO.CampaignDonateeDTO actionTypeDTO)
//         {
//             // Create actionType
//             var bllEntity = _mapper.Map(actionTypeDTO);
//             _bll.CampaignDonatees.Add(bllEntity);
//             
//             // var actionType = new CampaignDonatee
//             // {
//             //     CampaignDonateeValue = actionTypeCreateDTO.CampaignDonateeValue,
//             //     Comment = actionTypeCreateDTO.Comment
//             // };
//
//             await _bll.SaveChangesAsync();
//
//             actionTypeDTO.Id = bllEntity.Id;
//             return CreatedAtAction(
//                 "GetCampaignDonatee",
//                 new {id = actionTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
//                 actionTypeDTO
//                 );
//         }
//
//         // DELETE: api/CampaignDonatees/5
//         /// <summary>
//         ///     Delete CampaignDonatee
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//         [HttpDelete("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
//         public async Task<ActionResult<V1DTO.CampaignDonateeDTO>> DeleteCampaignDonatee(Guid id)
//         {
//             var actionType = await _bll.CampaignDonatees.FirstOrDefaultAsync(id, User.UserGuidId());
//             if (actionType == null)
//             {
//                 _logger.LogError($"DELETE. No such actionType: {id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"CampaignDonatee with id {id} not found"));
//             }
//             await _bll.CampaignDonatees.RemoveAsync(id);
//
//             await _bll.SaveChangesAsync();
//             return Ok(actionType);
//         }
//
//         // // GET: api/CampaignDonatees
//         // [HttpGet]
//         // public async Task<ActionResult<IEnumerable<V1DTO.CampaignDonateeDTO>>> GetCampaignDonatees()
//         // {
//         //     return Ok(await _uow.CampaignDonatees.DTOAllAsync());
//         // }
//         //
//         // // GET: api/CampaignDonatees/5
//         // [HttpGet("{id}")]
//         // public async Task<ActionResult<V1DTO.CampaignDonateeDTO>> GetCampaignDonatee(Guid id)
//         // {
//         //     var campaignDonatee = await _uow.CampaignDonatees.DTOFirstOrDefaultAsync(id);
//         //     if (campaignDonatee == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     return campaignDonatee;
//         // }
//         //
//         // // PUT: api/CampaignDonatees/5
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPut("{id}")]
//         // public async Task<IActionResult> PutCampaignDonatee(Guid id, CampaignDonatee campaignDonatee)
//         // {
//         //     if (id != campaignDonatee.Id)
//         //     {
//         //         return BadRequest();
//         //     }
//         //
//         //     _context.Entry(campaignDonatee).State = EntityState.Modified;
//         //
//         //     try
//         //     {
//         //         await _context.SaveChangesAsync();
//         //     }
//         //     catch (DbUpdateConcurrencyException)
//         //     {
//         //         if (!CampaignDonateeExists(id))
//         //         {
//         //             return NotFound();
//         //         }
//         //         else
//         //         {
//         //             throw;
//         //         }
//         //     }
//         //
//         //     return NoContent();
//         // }
//         //
//         // // POST: api/CampaignDonatees
//         // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // // more details see https://aka.ms/RazorPagesCRUD.
//         // [HttpPost]
//         // public async Task<ActionResult<CampaignDonatee>> PostCampaignDonatee(CampaignDonatee campaignDonatee)
//         // {
//         //     _context.CampaignDonatees.Add(campaignDonatee);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return CreatedAtAction("GetCampaignDonatee", new { id = campaignDonatee.Id }, campaignDonatee);
//         // }
//         //
//         // // DELETE: api/CampaignDonatees/5
//         // [HttpDelete("{id}")]
//         // public async Task<ActionResult<CampaignDonatee>> DeleteCampaignDonatee(Guid id)
//         // {
//         //     var campaignDonatee = await _context.CampaignDonatees.FindAsync(id);
//         //     if (campaignDonatee == null)
//         //     {
//         //         return NotFound();
//         //     }
//         //
//         //     _context.CampaignDonatees.Remove(campaignDonatee);
//         //     await _context.SaveChangesAsync();
//         //
//         //     return campaignDonatee;
//         // }
//         //
//         // private bool CampaignDonateeExists(Guid id)
//         // {
//         //     return _context.CampaignDonatees.Any(e => e.Id == id);
//         // }
//     }
// }
