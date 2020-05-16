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
            // wishlist.WishlistValue = wishlistEditDTO.WishlistValue;
            // wishlist.Comment = wishlistEditDTO.Comment;
            await _bll.Wishlists.UpdateAsync(_mapper.Map(wishlistDTO), User.UserId());

            // Save to db
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Wishlists.ExistsAsync(id, User.UserGuidId()))
                {
                    _logger.LogError(
                        $"EDIT. Wishlist does not exist - cannot save to db: {id}, user: {User.UserGuidId()}");
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Wishlists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Wishlist
        /// </summary>
        /// <param name="wishlistDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.WishlistDTO))]
        public async Task<ActionResult<V1DTO.WishlistDTO>> PostWishlist(V1DTO.WishlistDTO wishlistDTO)
        {
            // Create wishlist
            var bllEntity = _mapper.Map(wishlistDTO);
            _bll.Wishlists.Add(bllEntity);
            
            // var wishlist = new Wishlist
            // {
            //     WishlistValue = wishlistCreateDTO.WishlistValue,
            //     Comment = wishlistCreateDTO.Comment
            // };

            await _bll.SaveChangesAsync();

            wishlistDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetWishlist",
                new {id = wishlistDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                wishlistDTO
                );
        }

        // DELETE: api/Wishlists/5
        /// <summary>
        ///     Delete Wishlist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.CampaignDTO>))]
        public async Task<ActionResult<V1DTO.WishlistDTO>> DeleteWishlist(Guid id)
        {
            var wishlist = await _bll.Wishlists.FirstOrDefaultAsync(id, User.UserGuidId());
            if (wishlist == null)
            {
                _logger.LogError($"DELETE. No such wishlist: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Wishlist with id {id} not found"));
            }
            await _bll.Wishlists.RemoveAsync(id);

            await _bll.SaveChangesAsync();
            return Ok(wishlist);
        }

        // // GET: api/Wishlists
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<V1DTO.WishlistDTO>>> GetWishlists()
        // {
        //     return await _context.Wishlists
        //         .Select(w => new V1DTO.WishlistDTO()
        //         {
        //             Id = w.Id,
        //             Comment = w.Comment,
        //             GiftId = w.GiftId,
        //             ProfilesCount = w.Profiles.Count
        //         }).ToListAsync();
        // }
        //
        // // GET: api/Wishlists/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<V1DTO.WishlistDTO>> GetWishlist(Guid id)
        // {
        //     var wishlist = await _context.Wishlists
        //         .Select(w => new V1DTO.WishlistDTO()
        //         {
        //             Id = w.Id,
        //             Comment = w.Comment,
        //             GiftId = w.GiftId,
        //             ProfilesCount = w.Profiles.Count
        //         }).SingleOrDefaultAsync();
        //
        //     if (wishlist == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return wishlist;
        // }
        //
        // // PUT: api/Wishlists/5
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutWishlist(Guid id, Wishlist wishlist)
        // {
        //     if (id != wishlist.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(wishlist).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!WishlistExists(id))
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
        // // POST: api/Wishlists
        // // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // // more details see https://aka.ms/RazorPagesCRUD.
        // [HttpPost]
        // public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist wishlist)
        // {
        //     _context.Wishlists.Add(wishlist);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetWishlist", new { id = wishlist.Id }, wishlist);
        // }
        //
        // // DELETE: api/Wishlists/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Wishlist>> DeleteWishlist(Guid id)
        // {
        //     var wishlist = await _context.Wishlists.FindAsync(id);
        //     if (wishlist == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Wishlists.Remove(wishlist);
        //     await _context.SaveChangesAsync();
        //
        //     return wishlist;
        // }
        //
        // private bool WishlistExists(Guid id)
        // {
        //     return _context.Wishlists.Any(e => e.Id == id);
        // }
    }
}
