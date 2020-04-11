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
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WishlistsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public WishlistsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDTO>>> GetWishlists()
        {
            return Ok(await _uow.Wishlists.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Wishlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishlistDTO>> GetWishlist(Guid id)
        {
            var wishlistDTO = await _uow.Wishlists.DTOFirstOrDefaultAsync(id, User.UserGuidId());

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

            var wishlist = await _uow.Wishlists.FirstOrDefaultAsync(wishlistEditDTO.Id, User.UserGuidId());
            if (wishlist == null)
            {
                return BadRequest();
            }
            
            wishlist.Comment = wishlistEditDTO.Comment;
            wishlist.AppUserId = wishlistEditDTO.AppUserId;

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
            var wishlist = new Wishlist
            {
                Comment = wishlistCreateDTO.Comment,
                AppUserId = User.UserGuidId()
            };
            
            _uow.Wishlists.Add(wishlist);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetWishlist", new { id = wishlist.Id }, wishlist);
        }

        // DELETE: api/Wishlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wishlist>> DeleteWishlist(Guid id)
        {
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
