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
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class WishlistsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<WishlistsController> _logger;
        private readonly WishlistMapper _mapper = new WishlistMapper();

        public WishlistsController(IAppBLL appBLL, ILogger<WishlistsController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Wishlists
        /// <summary>
        ///     Get all Wishlists
        /// </summary>
        /// <returns>List of Wishlists</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.WishlistDTO>>> GetWishlists()
        {
            return Ok((await _bll.Wishlists.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Wishlists/5
        /// <summary>
        ///     Get a single Wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Wishlist object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.WishlistDTO>> GetWishlist(Guid id)
        {
            var wishlist = await _bll.Wishlists.FirstOrDefaultAsync(id);
            if (wishlist == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Wishlist with id {id} not found"));
            }

            return Ok(_mapper.Map(wishlist));
        }
        
        // GET: api/Wishlists/personal/5
        /// <summary>
        ///     Get a single personal Wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Wishlist object</returns>
        [HttpGet("personal/{id?}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.WishlistDTO>))]
        public async Task<ActionResult<V1DTO.WishlistDTO>> GetPersonalWishlist(Guid? id)
        {
            // TODO: Move to BLL
            var wishlistId = id ?? default;
            
            if (wishlistId == Guid.Empty)
            {
                var firstPersonalWishlist = (await _bll.Wishlists.GetAllAsync())
                    .Where(w => w.AppUserId == User.UserGuidId())
                    .Select(e => _mapper.Map(e))
                    .FirstOrDefault();
                return Ok(firstPersonalWishlist);
            }
            
            var wishlist = await _bll.Wishlists.FirstOrDefaultAsync(wishlistId);
            if (wishlist == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Wishlist with id {id} not found"));
            }
            if (wishlist.AppUserId != User.UserGuidId())
            {
                return NotFound(new V1DTO.MessageDTO($"Wishlist with id {id} not found"));
            }
            return Ok(_mapper.Map(wishlist));
        }

        // PUT: api/Wishlists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="wishlistDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutWishlist(Guid id, V1DTO.WishlistDTO wishlistDTO)
        {
            // Don't allow wrong data
            if (id != wishlistDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and wishlist.id do not match"));
            }
            var wishlist = await _bll.Wishlists.FirstOrDefaultAsync(wishlistDTO.Id, User.UserGuidId());
            if (wishlist == null)
            {
                _logger.LogError($"EDIT. No such wishlist: {wishlistDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Wishlist found for id {id}"));
            }
            // Update existing wishlist
            await _bll.Wishlists.UpdateAsync(_mapper.Map(wishlistDTO), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
