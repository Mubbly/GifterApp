using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuizTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuizTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizType>>> GetQuizTypes()
        {
            return await _context.QuizTypes.ToListAsync();
        }

        // GET: api/QuizTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizType>> GetQuizType(Guid id)
        {
            var quizType = await _context.QuizTypes.FindAsync(id);

            if (quizType == null)
            {
                return NotFound();
            }

            return quizType;
        }

        // PUT: api/QuizTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizType(Guid id, QuizType quizType)
        {
            if (id != quizType.Id)
            {
                return BadRequest();
            }

            _context.Entry(quizType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizTypeExists(id))
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

        // POST: api/QuizTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<QuizType>> PostQuizType(QuizType quizType)
        {
            _context.QuizTypes.Add(quizType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuizType", new { id = quizType.Id }, quizType);
        }

        // DELETE: api/QuizTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizType>> DeleteQuizType(Guid id)
        {
            var quizType = await _context.QuizTypes.FindAsync(id);
            if (quizType == null)
            {
                return NotFound();
            }

            _context.QuizTypes.Remove(quizType);
            await _context.SaveChangesAsync();

            return quizType;
        }

        private bool QuizTypeExists(Guid id)
        {
            return _context.QuizTypes.Any(e => e.Id == id);
        }
    }
}
