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
//     public class ReservedGiftsController : ControllerBase
//     {
//         private readonly IAppBLL _bll;
//         private readonly ILogger<ReservedGiftsController> _logger;
//         private readonly ReservedGiftMapper _mapper = new ReservedGiftMapper();
//
//         public ReservedGiftsController(IAppBLL appBLL, ILogger<ReservedGiftsController> logger)
//         {
//             _bll = appBLL;
//             _logger = logger;
//         }
//         
//                 // GET: api/ReservedGifts
//         /// <summary>
//         ///     Get all ReservedGifts
//         /// </summary>
//         /// <returns>List of ReservedGifts</returns>
//         [HttpGet]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ReservedGiftResponseDTO>))]
//         public async Task<ActionResult<IEnumerable<V1DTO.ReservedGiftDTO>>> GetReservedGifts()
//         {
//             return Ok((await _bll.ReservedGifts.GetAllAsync()).Select(e => _mapper.Map(e)));
//         }
//
//         // GET: api/ReservedGifts/5
//         /// <summary>
//         ///     Get a single ReservedGift
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns>ReservedGift object</returns>
//         [HttpGet("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ReservedGiftResponseDTO>))]
//         public async Task<ActionResult<V1DTO.ReservedGiftDTO>> GetReservedGift(Guid id)
//         {
//             var reservedGift = await _bll.ReservedGifts.FirstOrDefaultAsync(id);
//             if (reservedGift == null)
//             {
//                 return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {id.ToString()} not found"));
//             }
//             return Ok(_mapper.Map(reservedGift));
//         }
//
//         // PUT: api/ReservedGifts/5
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Update ReservedGift
//         /// </summary>
//         /// <param name="id"></param>
//         /// <param name="reservedGiftDTO"></param>
//         /// <returns></returns>
//         [HttpPut("{id}")]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         public async Task<IActionResult> PutReservedGift(Guid id, V1DTO.ReservedGiftDTO reservedGiftDTO)
//         {
//             // Don't allow wrong data
//             if (id != reservedGiftDTO.Id)
//             {
//                 return BadRequest(new V1DTO.MessageDTO("id and reservedGift.id do not match"));
//             }
//             var reservedGift = await _bll.ReservedGifts.FirstOrDefaultAsync(reservedGiftDTO.Id, User.UserGuidId());
//             if (reservedGift == null)
//             {
//                 _logger.LogError($"EDIT. No such reservedGift: {reservedGiftDTO.Id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"No ReservedGift found for id {id}"));
//             }
//             // Update existing reservedGift
//             await _bll.ReservedGifts.UpdateAsync(_mapper.Map(reservedGiftDTO), User.UserId());
//             await _bll.SaveChangesAsync();
//
//             return NoContent();
//         }
//
//         // POST: api/ReservedGifts
//         // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//         // more details see https://aka.ms/RazorPagesCRUD.
//         /// <summary>
//         ///     Add new ReservedGift
//         /// </summary>
//         /// <param name="reservedGiftDTO"></param>
//         /// <returns></returns>
//         [HttpPost]
//         [Produces("application/json")]
//         [Consumes("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ReservedGiftDTO))]
//         public async Task<ActionResult<V1DTO.ReservedGiftResponseDTO>> PostReservedGift(V1DTO.ReservedGiftDTO reservedGiftDTO)
//         {
//             // Create reservedGift
//             var bllEntity = _mapper.Map(reservedGiftDTO);
//             await _bll.ReservedGifts.Add(bllEntity, User.UserGuidId());
//             // Save to db
//             await _bll.SaveChangesAsync();
//             
//             reservedGiftDTO.Id = bllEntity.Id;
//             return CreatedAtAction(
//                 "GetReservedGift",
//                 new {id = reservedGiftDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
//                 reservedGiftDTO
//                 );
//         }
//
//         // DELETE: api/ReservedGifts/5
//         /// <summary>
//         ///     Delete ReservedGift
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//         [HttpDelete("{id}")]
//         [Produces("application/json")]
//         [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
//         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ReservedGiftResponseDTO>))]
//         public async Task<ActionResult<V1DTO.ReservedGiftResponseDTO>> DeleteReservedGift(Guid id)
//         {
//             var reservedGift = await _bll.ReservedGifts.FirstOrDefaultAsync(id, User.UserGuidId());
//             if (reservedGift == null)
//             {
//                 _logger.LogError($"DELETE. No such reservedGift: {id}, user: {User.UserGuidId()}");
//                 return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {id} not found"));
//             }
//             
//             // TODO: Update corresponding Gift in gifts table to have isReserved=false
//
//             await _bll.ReservedGifts.RemoveAsync(id);
//             await _bll.SaveChangesAsync();
//             return Ok(reservedGift);
//         }
//     }
// }
