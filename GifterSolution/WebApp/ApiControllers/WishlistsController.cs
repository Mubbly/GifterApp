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
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WishlistsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ILogger<CampaignsController> _logger;
        
        public WishlistsController(IAppUnitOfWork uow, ILogger<CampaignsController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetWishlists()
        {
            // Allow all users see all wishlists
            return Ok(await _uow.Wishlists.DTOAllAsync());
        }
        
        // GET: api/Wishlists/Personal
        [HttpGet("personal")]
        public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetPersonalWishlists()
        {
            // Get only your own wishlists
            return Ok(await _uow.Wishlists.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Wishlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishlistDTO>> GetWishlist(Guid id)
        {
            // Allow all users see all wishlists
            var wishlistDTO = await _uow.Wishlists.DTOFirstOrDefaultAsync(id);
            if (wishlistDTO == null)
            {
                return NotFound();
            }
            return Ok(wishlistDTO);
        }

        // PUT: api/Wishlists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishlist(Guid id, WishlistEditDTO wishlistEditDTO)
        {
            if (id != wishlistEditDTO.Id)
            {
                return BadRequest();
            }

            // Only allow users to edit their own wishlist
            var wishlist = await _uow.Wishlists.FirstOrDefaultAsync(wishlistEditDTO.Id, User.UserGuidId());
            if (wishlist == null)
            {
                return BadRequest();
            }
            
            wishlist.Comment = wishlistEditDTO.Comment;
            wishlist.AppUserId = User.UserGuidId();

            _uow.Wishlists.Update(wishlist);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Wishlists.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Wishlists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WishlistCreateDTO>> PostWishlist(WishlistCreateDTO wishlistCreateDTO)
        {
            // Allow all users create wishlists
            var wishlist = new Wishlist
            {
                Comment = wishlistCreateDTO.Comment,
                AppUserId = User.UserGuidId()
            };
            
            _uow.Wishlists.Add(wishlist);
            await _uow.SaveChangesAsync();

            // Send response back to user
            wishlistCreateDTO.Id = wishlist.Id;
            return CreatedAtAction("GetWishlist", new { id = wishlist.Id }, wishlistCreateDTO);
        }

        // DELETE: api/Wishlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wishlist>> DeleteWishlist(Guid id)
        {
            // Only allow users to delete their own wishlist
            var wishlist = await _uow.Wishlists.FirstOrDefaultAsync(id, User.UserGuidId());
            if (wishlist == null)
            {
                return NotFound();
            }

            _uow.Wishlists.Remove(wishlist);
            await _uow.SaveChangesAsync();

            return Ok(wishlist);
        }
    }
}
