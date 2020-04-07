using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCampaignsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserCampaignsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserCampaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCampaignDTO>>> GetUserCampaigns()
        {
            return await _context.UserCampaigns
                .Select(uc => new UserCampaignDTO() 
                {
                    Id = uc.Id,
                    Comment = uc.Comment,
                    CampaignId = uc.CampaignId,
                    AppUserId = uc.AppUserId
                }).ToListAsync();
        }

        // GET: api/UserCampaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCampaignDTO>> GetUserCampaign(Guid id)
        {
            var userCampaign = await _context.UserCampaigns
                .Select(uc => new UserCampaignDTO() 
                {
                    Id = uc.Id,
                    Comment = uc.Comment,
                    CampaignId = uc.CampaignId,
                    AppUserId = uc.AppUserId
                }).SingleOrDefaultAsync();

            if (userCampaign == null)
            {
                return NotFound();
            }

            return userCampaign;
        }

        // PUT: api/UserCampaigns/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCampaign(Guid id, UserCampaign userCampaign)
        {
            if (id != userCampaign.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCampaign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCampaignExists(id))
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

        // POST: api/UserCampaigns
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserCampaign>> PostUserCampaign(UserCampaign userCampaign)
        {
            _context.UserCampaigns.Add(userCampaign);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCampaign", new { id = userCampaign.Id }, userCampaign);
        }

        // DELETE: api/UserCampaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserCampaign>> DeleteUserCampaign(Guid id)
        {
            var userCampaign = await _context.UserCampaigns.FindAsync(id);
            if (userCampaign == null)
            {
                return NotFound();
            }

            _context.UserCampaigns.Remove(userCampaign);
            await _context.SaveChangesAsync();

            return userCampaign;
        }

        private bool UserCampaignExists(Guid id)
        {
            return _context.UserCampaigns.Any(e => e.Id == id);
        }
    }
}
