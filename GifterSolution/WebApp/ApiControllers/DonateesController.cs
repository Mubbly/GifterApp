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
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonateesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public DonateesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Donatees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonateeDTO>>> GetDonatees()
        {
            return Ok(await _uow.Donatees.DTOAllAsync());
        }

        // GET: api/Donatees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonateeDTO>> GetDonatee(Guid id)
        {
            var donatee = await _uow.Donatees.DTOFirstOrDefaultAsync(id);

            if (donatee == null)
            {
                return NotFound();
            }

            return donatee;
        }

        // PUT: api/Donatees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonatee(Guid id, DonateeEditDTO donateeEditDTO)
        {
            if (id != donateeEditDTO.Id)
            {
                return BadRequest();
            }

            var donatee = await _uow.Donatees.FirstOrDefaultAsync(donateeEditDTO.Id);
            if (donatee == null)
            {
                return BadRequest();
            }
            donatee.FirstName = donateeEditDTO.FirstName;
            donatee.LastName = donateeEditDTO.LastName;
            donatee.Age = donateeEditDTO.Age;
            donatee.Gender = donateeEditDTO.Gender;
            donatee.Bio = donateeEditDTO.Bio;
            donatee.GiftName = donateeEditDTO.GiftName;
            donatee.GiftDescription = donateeEditDTO.GiftDescription;
            donatee.GiftImage = donateeEditDTO.GiftImage;
            donatee.GiftUrl = donateeEditDTO.GiftUrl;
            donatee.ActiveFrom = donateeEditDTO.ActiveFrom;
            donatee.ActiveTo = donateeEditDTO.ActiveTo;
            donatee.ActionTypeId = donateeEditDTO.ActionTypeId;
            donatee.StatusId = donateeEditDTO.StatusId;

            _uow.Donatees.Update(donatee);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Donatees.ExistsAsync(id))
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

        // POST: api/Donatees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Donatee>> PostDonatee(DonateeCreateDTO donateeCreateDTO)
        {
            var donatee = new Donatee
            {
                Id = donateeCreateDTO.Id,
                FirstName = donateeCreateDTO.FirstName,
                LastName = donateeCreateDTO.LastName,
                Age = donateeCreateDTO.Age,
                Gender = donateeCreateDTO.Gender,
                Bio = donateeCreateDTO.Bio,
                GiftName = donateeCreateDTO.GiftName,
                GiftDescription = donateeCreateDTO.GiftDescription,
                GiftImage = donateeCreateDTO.GiftImage,
                GiftUrl = donateeCreateDTO.GiftUrl,
                ActiveFrom = donateeCreateDTO.ActiveFrom,
                ActiveTo = donateeCreateDTO.ActiveTo,
                ActionTypeId = donateeCreateDTO.ActionTypeId,
                StatusId = donateeCreateDTO.StatusId
            };
            
            _uow.Donatees.Add(donatee);
            
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDonatee", new { id = donatee.Id }, donatee);
        }

        // DELETE: api/Donatees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Donatee>> DeleteDonatee(Guid id)
        {
            var donatee = await _uow.Donatees.FirstOrDefaultAsync(id);
            if (donatee == null)
            {
                return NotFound();
            }

            _uow.Donatees.Remove(donatee);
            await _uow.SaveChangesAsync();

            return Ok(donatee);
        }
    }
}
