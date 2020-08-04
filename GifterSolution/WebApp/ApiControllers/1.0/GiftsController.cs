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
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using WebApp.Helpers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    ///     Gifts controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class GiftsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<GiftsController> _logger;
        private readonly GiftMapper _mapper = new GiftMapper();

        // Statuses
        private static string _activeId = "";
        private static string _reservedId = "";
        private static string _archivedId = "";
        // Actiontypes
        private static string _archiveId = "";
        private static string _reserveId = "";

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="appBLL"></param>
        /// <param name="logger"></param>
        public GiftsController(IAppBLL appBLL, ILogger<GiftsController> logger)
        {
            _bll = appBLL;
            _logger = logger;

            // Get necessary predefined statuses & actionTypes
            var enums = new Enums();
            _activeId = enums.GetStatusId(Enums.Status.Active);
            _reservedId = enums.GetStatusId(Enums.Status.Reserved);
            _archivedId = enums.GetStatusId(Enums.Status.Archived);
            _archiveId = enums.GetActionTypeId(Enums.ActionType.Archive);
            _reserveId = enums.GetActionTypeId(Enums.ActionType.Reserve);
        }

        // GET: api/Gifts/User/5
        /// <summary>
        ///     Get all gifts for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetGifts(Guid userId)
        {
            var personalGifts = await _bll.Gifts.GetAllInWishlistForUserAsync(userId);
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gifts not found"));
            }

            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }

        // GET: api/Gifts/Pinned/User/5
        /// <summary>
        ///     Get all gifts for certain user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("pinned/user/{userId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetPinnedGifts(Guid userId)
        {
            var personalGifts = await _bll.Gifts.GetAllPinnedForUserAsync(userId);
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gifts not found"));
            }

            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }

        // GET: api/Gifts/Personal
        /// <summary>
        ///     Get all personal gifts
        /// </summary>
        /// <returns></returns>
        [HttpGet("personal")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.GiftDTO))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetPersonalGifts()
        {
            var personalGifts = await _bll.Gifts.GetAllInWishlistForUserAsync(User.UserGuidId());
            if (personalGifts == null)
            {
                return NotFound(new V1DTO.MessageDTO("Gifts not found"));
            }

            return Ok(personalGifts.Select(e => _mapper.Map(e)));
        }

        // // GET: api/Gifts/5
        // /// <summary>
        // ///     Get a single gift
        // /// </summary>
        // /// <param name="id"></param>
        // /// <returns></returns>
        // [HttpGet("{id}")]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        // public async Task<ActionResult<V1DTO.GiftDTO>> GetGift(Guid id)
        // {
        //     var gift = await _bll.Gifts.FirstOrDefaultAsync(id);
        //     if (gift == null)
        //     {
        //         return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
        //     }
        //     return Ok(_mapper.Map(gift));
        // }

        // GET: api/Gifts/Personal/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("personal/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetPersonalGift(Guid id)
        {
            var gift = await _bll.Gifts.GetForUserAsync(id, User.UserGuidId());
            if (gift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Gift with id {id.ToString()} not found"));
            }
            return Ok(_mapper.Map(gift));
        }

        // PUT: api/Gifts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Edit existing gift
        /// </summary>
        /// <param name="id"></param>
        /// <param name="giftDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<IActionResult> PutGift(Guid id, V1DTO.GiftDTO giftDTO)
        {
            // Don't allow wrong data
            if (id != giftDTO.Id)
            {
                _logger.LogError($"EDIT. Gift IDs do not match: giftId {giftDTO.Id}, id {id}");
                return BadRequest(new V1DTO.MessageDTO($"Could not change gift with this id: {id}"));
            }

            var gift = await _bll.Gifts.FirstOrDefaultAsync(giftDTO.Id, User.UserGuidId());
            if (gift == null)
            {
                _logger.LogError($"EDIT. No such gift: {giftDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No gift found for id {id}"));
            }

            // Allow changing own gifts only
            var personalGift = await _bll.Gifts.GetForUserAsync(id, User.UserGuidId());
            if (personalGift == null)
            {
                _logger.LogError($"EDIT. Gift {giftDTO.Id} is not owned by user {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No gift found for id {id}"));
            }

            // Update existing Gift
            await _bll.Gifts.UpdateAsync(_mapper.Map(giftDTO), User.UserGuidId());
            // Save changes to db
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Gifts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        ///     Add new gift
        /// </summary>
        /// <param name="giftDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> PostGift(V1DTO.GiftDTO giftDTO)
        {
            // Create gift
            var bllEntity = _mapper.Map(giftDTO);
            // TODO: Move to BLL
            bllEntity.StatusId = new Guid(_activeId); // Active
            bllEntity.ActionTypeId = new Guid(_reservedId); // Reserve
            _bll.Gifts.Add(bllEntity, User.UserGuidId());
            // Save to db
            await _bll.SaveChangesAsync();

            giftDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetGift",
                new {id = giftDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                giftDTO
            );
        }

        // DELETE: api/Gifts/5
        /// <summary>
        ///     Delete gift
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ActionTypeDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> DeleteGift(Guid id)
        {
            var gift = await _bll.Gifts.FirstOrDefaultAsync(id, User.UserGuidId());
            if (gift == null)
            {
                _logger.LogError($"DELETE. No such gift: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Gift with id {id} not found"));
            }

            await _bll.Gifts.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(gift);
        }

        // -------------------------------------------- RESERVED GIFTS ------------------------------------------

        // GET: api/Gifts/Reserved/Personal
        /// <summary>
        ///     Get all Gifts that current user has reserved
        /// </summary>
        /// <returns>List of Gifts</returns>
        [HttpGet("reserved/personal")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetAllPersonalReservedGifts()
        {
            var personalReservedGifts = (await _bll.Gifts.GetAllReservedForUserAsync(User.UserGuidId()))
                .Select(e => _mapper.Map(e));
            return Ok(personalReservedGifts);
        }

        // GET: api/Gifts/Reserved/Personal/5
        /// <summary>
        ///     Get a single Gift that current user has reserved
        /// </summary>
        /// <param name="giftId"></param>
        /// <returns>Gift object</returns>
        [HttpGet("reserved/personal/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetPersonalReservedGift(Guid giftId)
        {
            var reservedGift = await _bll.Gifts.GetReservedForUserAsync(giftId, User.UserGuidId());
            if (reservedGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"ReservedGift with giftId {giftId.ToString()} not found"));
            }
            return Ok(_mapper.Map(reservedGift));
        }
        
        // POST: api/Gifts/Reserved
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new ReservedGift - update Gift to Reserved status
        /// </summary>
        /// <param name="reservedGiftDTO"></param>
        /// <returns></returns>
        [HttpPost("reserved")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GiftDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> PostReservedGift(V1DTO.ReservedGiftDTO reservedGiftDTO)
        {
            if (reservedGiftDTO.GiftId.Equals(Guid.Empty) || reservedGiftDTO.UserReceiverId.Equals(Guid.Empty))
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {reservedGiftDTO.GiftId.ToString()}"));
            }
            var bllEntity = _mapper.MapReservedGiftDTOToBLL(reservedGiftDTO);
            // Reserve gift
            var giftDTO = _mapper.Map(await _bll.Gifts.MarkAsReservedAsync(bllEntity, User.UserGuidId()));
            // Save to db
            await _bll.SaveChangesAsync();

            // Send back updated Gift with additional info, not created ReservedGift (it is just an implementation detail)
            return CreatedAtAction(
                "GetReservedGift",
                new {id = bllEntity.GiftId, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                giftDTO
            );
        }
        
        // PUT: api/Gifts/Reserved/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update ReservedGift - Complete reservation, update Gift to Archived ("Gifted") status
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="reservedGiftDTO"></param>
        /// <returns></returns>
        [HttpPut("reserved/{giftId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutReservedGift(Guid giftId, V1DTO.ReservedGiftDTO reservedGiftDTO)
        {
            // Don't allow wrong data
            if (reservedGiftDTO.GiftId.Equals(Guid.Empty) || reservedGiftDTO.UserReceiverId.Equals(Guid.Empty) 
                                                          || giftId != reservedGiftDTO.GiftId)
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()} or {reservedGiftDTO.GiftId}"));
            }
            // Mark gifted and archive
            var giftDTO = _mapper.Map(await _bll.Gifts.MarkAsGiftedAsync(_mapper.MapReservedGiftDTOToBLL(reservedGiftDTO), User.UserGuidId()));
            if (giftDTO == null)
            {
                _logger.LogError($"EDIT. No such reserved Gift: {reservedGiftDTO.GiftId}, user: {User.UserGuidId().ToString()}");
                return NotFound(new V1DTO.MessageDTO($"No reserved Gift found for id {giftId.ToString()}"));
            }
            // Save updates to db
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        // DELETE: api/Gifts/Reserved/5
        /// <summary>
        ///     Delete ReservedGift - Cancel reservation, change Gift back to active status
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="reservedGiftDTO"></param>
        /// <returns></returns>
        [HttpDelete("reserved/{giftId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> DeleteReservedGift(Guid giftId,
            V1DTO.ReservedGiftDTO reservedGiftDTO)
        {
            // Don't allow wrong data
            if (reservedGiftDTO.GiftId.Equals(Guid.Empty) || reservedGiftDTO.UserReceiverId.Equals(Guid.Empty) 
                                                          || giftId != reservedGiftDTO.GiftId)
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()} or {reservedGiftDTO.GiftId}"));
            }
            // Cancel reservation, reactivate Gift
            var giftDTO = _mapper.Map(await _bll.Gifts.CancelReservationAsync(_mapper.MapReservedGiftDTOToBLL(reservedGiftDTO), User.UserGuidId()));
            if (giftDTO == null)
            {
                _logger.LogError($"DELETE. No such reservedGift: {giftId.ToString()}, user: {User.UserGuidId().ToString()}");
                return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {giftId.ToString()} not found"));
            }
            // Save updates to db
            await _bll.SaveChangesAsync();
        
            // Send back updated Gift with additional info, not deleted ReservedGift (it is just an implementation detail)
            return Ok(giftDTO);
        }
        
        
        // -------------------------------------------- ARCHIVED GIFTS ------------------------------------------
        
        
        // GET: api/Gifts/Archived/Given
        /// <summary>
        ///     Get all Archived Gifts that current user has given to others
        /// </summary>
        /// <returns>List of Gifts</returns>
        [HttpGet("archived/given")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetAllGivenArchivedGifts()
        {
            var givenGifts = (await _bll.Gifts.GetAllArchivedForUserAsync(User.UserGuidId(), true))
                .Select(e => _mapper.Map(e));
            return Ok(givenGifts);
        }
        
        // GET: api/Gifts/Archived/Received
        /// <summary>
        ///     Get all Archived Gifts that current user has received from others
        /// </summary>
        /// <returns>List of Gifts</returns>
        [HttpGet("archived/received")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetAllReceivedArchivedGifts()
        {
            var receivedGifts = (await _bll.Gifts.GetAllArchivedForUserAsync(User.UserGuidId(), false))
                .Select(e => _mapper.Map(e));
            return Ok(receivedGifts);
        }
        
        // GET: api/Gifts/Archived/Received/Pending
        /// <summary>
        ///     Get all Archived Gifts that current user has received from others
        /// </summary>
        /// <returns>List of Gifts</returns>
        [HttpGet("archived/received/pending")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.GiftDTO>>> GetAllPendingReceivedArchivedGifts()
        {
            var pendingReceivedGifts = (await _bll.Gifts.GetAllPendingArchivedForUserAsync(User.UserGuidId(), false))
                .Select(e => _mapper.Map(e));
            return Ok(pendingReceivedGifts);
        }

        // GET: api/Gifts/Archived/Given/5
        /// <summary>
        ///     Get a single Gift that current user has reserved
        /// </summary>
        /// <param name="giftId"></param>
        /// <returns>Gift object</returns>
        [HttpGet("archived/given/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetGivenArchivedGifts(Guid giftId)
        {
            var givenGift = await _bll.Gifts.GetArchivedForUserAsync(giftId, User.UserGuidId(), true);
            if (givenGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Archived Gift  with giftId {giftId.ToString()} not found"));
            }
            return Ok(_mapper.Map(givenGift));
        }
        
        // GET: api/Gifts/Archived/Received/5
        /// <summary>
        ///     Get a single Gift that current user has reserved
        /// </summary>
        /// <param name="giftId"></param>
        /// <returns>Gift object</returns>
        [HttpGet("archived/received/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.GiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> GetReceivedArchivedGifts(Guid giftId)
        {
            var receivedGift = await _bll.Gifts.GetArchivedForUserAsync(giftId, User.UserGuidId(), false);
            if (receivedGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Archived Gift with giftId {giftId.ToString()} not found"));
            }
            return Ok(_mapper.Map(receivedGift));
        }
        
        // PUT: api/Gifts/Archived/Pending/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update pending Archived Gift - confirm archival
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="archivedGiftDTO"></param>
        /// <returns></returns>
        [HttpPut("archived/pending/{giftId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutPendingArchivedGift(Guid giftId, V1DTO.ArchivedGiftDTO archivedGiftDTO)
        {
            // Don't allow wrong data
            if (archivedGiftDTO.GiftId.Equals(Guid.Empty) 
                || archivedGiftDTO.UserGiverId.Equals(Guid.Empty)
                || giftId != archivedGiftDTO.GiftId)
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()} or {archivedGiftDTO.GiftId}"));
            }
            // Make sure target gift exists and is in pending archival status
            var targetGift = await _bll.Gifts.GetPendingArchivedForUserAsync(giftId, User.UserGuidId());
            if(targetGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()}"));
            }
            // Confirm getting this gift / archive it
            var confirmedArchivedGift = _mapper.Map(await _bll.Gifts.ConfirmPendingArchivedAsync(_mapper.MapArchivedGiftDTOToBLL(archivedGiftDTO), User.UserGuidId()));
            if (confirmedArchivedGift == null)
            {
                _logger.LogError($"EDIT. No such pending received Gift: {archivedGiftDTO.GiftId}, user: {User.UserGuidId().ToString()}");
                return NotFound(new V1DTO.MessageDTO($"No pending received Gift found for id {giftId.ToString()}"));
            }
            // Save updates to db
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        // POST: api/Gifts/Reactivated
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     "Reactivate" archived entry - add new Gift with the same data that the existing confirmed archived gift has
        /// </summary>
        /// <param name="archivedGiftDTO"></param>
        /// <returns></returns>
        [HttpPost("reactivated")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.GiftDTO))]
        public async Task<ActionResult<V1DTO.GiftDTO>> PostGift(V1DTO.ArchivedGiftDTO archivedGiftDTO)
        {
            if (archivedGiftDTO.GiftId.Equals(Guid.Empty) || archivedGiftDTO.UserGiverId.Equals(Guid.Empty))
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {archivedGiftDTO.GiftId.ToString()}"));
            }
            // Make sure target gift exists and is in archived status
            var targetGift = await _bll.Gifts.GetArchivedForUserAsync(archivedGiftDTO.GiftId, User.UserGuidId());
            if(targetGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Could not find gift with id {archivedGiftDTO.GiftId.ToString()}"));
            }
            // Add new gift based on archived gift
            var archivedGiftBLL = _mapper.MapArchivedGiftDTOToBLL(archivedGiftDTO);
            var newGift = _mapper.Map(await _bll.Gifts.ReactivateArchivedAsync(archivedGiftBLL, User.UserGuidId()));
            if (newGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Could not find gift with id {archivedGiftDTO.GiftId.ToString()}"));
            }
            // Save to db
            await _bll.SaveChangesAsync();

            // Send back updated Gift
            return CreatedAtAction(
                "GetPersonalGift",
                new {id = archivedGiftBLL.GiftId, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                newGift
            );
        }

        // DELETE: api/Gifts/Archived/Pending/5
        /// <summary>
        ///     Delete pending Archived Gift - Deny archival, change Gift back to active status
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="archivedGiftDTO"></param>
        /// <returns></returns>
        [HttpDelete("archived/pending/{giftId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ArchivedGiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> DeletePendingArchivedGift(Guid giftId,
            V1DTO.ArchivedGiftDTO archivedGiftDTO)
        {
            // Don't allow wrong data
            if (archivedGiftDTO.GiftId.Equals(Guid.Empty) 
                || archivedGiftDTO.UserGiverId.Equals(Guid.Empty) 
                || giftId != archivedGiftDTO.GiftId)
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()} or {archivedGiftDTO.GiftId}"));
            }
            // Deny getting gift and reactivate it instead
            var reactivatedGift = _mapper.Map(await _bll.Gifts.DenyPendingArchivedAsync(_mapper.MapArchivedGiftDTOToBLL(archivedGiftDTO), User.UserGuidId()));
            if (reactivatedGift == null)
            {
                _logger.LogError($"DELETE. No such reservedGift: {giftId.ToString()}, user: {User.UserGuidId().ToString()}");
                return NotFound(new V1DTO.MessageDTO($"ReservedGift with id {giftId.ToString()} not found"));
            }
            // Save updates to db
            await _bll.SaveChangesAsync();
            
            // Send back updated Gift with additional info, not deleted ReservedGift (it is just an implementation detail)
            return Ok(reactivatedGift);
        }
        
        // DELETE: api/Gifts/Archived/5
        /// <summary>
        ///     Delete Archived Gift - Delete all info about this Gift (except from giver Friend's archive). Cannot be undone!
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="archivedGiftDTO"></param>
        /// <returns></returns>
        [HttpDelete("archived/{giftId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ArchivedGiftDTO>))]
        public async Task<ActionResult<V1DTO.GiftDTO>> DeleteArchivedGift(Guid giftId, V1DTO.ArchivedGiftDTO archivedGiftDTO) 
        {
            // Don't allow wrong data
            if (archivedGiftDTO.GiftId.Equals(Guid.Empty) 
                || archivedGiftDTO.UserGiverId.Equals(Guid.Empty) 
                || giftId != archivedGiftDTO.GiftId)
            {
                return BadRequest(new V1DTO.MessageDTO($"Could not find archived gift with id {giftId.ToString()} or {archivedGiftDTO.GiftId}"));
            }
            // Delete archived gift
            var deletedArchivedGift = _mapper.Map(await _bll.Gifts.DeleteArchivedAsync(_mapper.MapArchivedGiftDTOToBLL(archivedGiftDTO), User.UserGuidId()));
            if (deletedArchivedGift == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Could not find archived gift with id {giftId.ToString()}"));
            }
            // Save updates to db
            await _bll.SaveChangesAsync();
            
            // Send back updated Gift with additional info, not deleted ReservedGift (it is just an implementation detail)
            return Ok(deletedArchivedGift);
        }
        
        
        // // PUT: api/Gifts/Archived/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // /// <summary>
        // ///     Update confirmed Archived Gift - reactivate it
        // /// </summary>
        // /// <param name="giftId"></param>
        // /// <param name="archivedGiftDTO"></param>
        // /// <returns></returns>
        // [HttpPut("archived/{giftId}")]
        // [Produces("application/json")]
        // [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // public async Task<IActionResult> PutArchivedGift(Guid giftId, V1DTO.ArchivedGiftDTO archivedGiftDTO)
        // {
        //     // Don't allow wrong data
        //     if (archivedGiftDTO.GiftId.Equals(Guid.Empty) 
        //         || archivedGiftDTO.UserGiverId.Equals(Guid.Empty)
        //         || giftId != archivedGiftDTO.GiftId) {
        //         return BadRequest(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()} or {archivedGiftDTO.GiftId}"));
        //     }
        //     // Make sure target gift exists and is in archived status
        //     var targetGift = await _bll.Gifts.GetArchivedForUserAsync(giftId, User.UserGuidId());
        //     if(targetGift == null)
        //     {
        //         return NotFound(new V1DTO.MessageDTO($"Could not find gift with id {giftId.ToString()}"));
        //     }
        //     // Reactivate this gift
        //     var reactivatedGift = _mapper.Map(await _bll.Gifts.ReactivateArchivedAsync(_mapper.MapArchivedGiftDTOToBLL(archivedGiftDTO), User.UserGuidId()));
        //     if (reactivatedGift == null)
        //     {
        //         _logger.LogError($"EDIT. No such pending received Gift: {archivedGiftDTO.GiftId}, user: {User.UserGuidId().ToString()}");
        //         return NotFound(new V1DTO.MessageDTO($"No pending received Gift found for id {giftId.ToString()}"));
        //     }
        //     // Save updates to db
        //     await _bll.SaveChangesAsync();
        //     
        //     return NoContent();
        // }
    }
}