using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class QuizResponsesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuizResponsesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuizResponses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizResponse>>> GetQuizResponses()
        {
            return await _context.QuizResponses.ToListAsync();
        }
        
        // GET: api/QuizResponses/Quizzes/5
        [HttpGet("quizzes/{quizId}")]
        public async Task<ActionResult<IEnumerable<QuizResponse>>> GetQuizResponsesForQuiz(Guid quizId)
        {
            return await _context.QuizResponses
                .Where(e => e.QuizId == quizId)
                .ToListAsync();
        }
        
        // GET: api/QuizResponses/Quizzes/5
        [HttpGet("quizzes/{quizId}/report")]
        public async Task<ActionResult<QuizReportDTO>> GetQuizResponsesReport(Guid quizId)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz == null)
            {
                return NotFound();
            }

            // Create a report showing how many people responded to which answers (and on which questions) under given quiz plus other data
            var quizReport = await GetQuizReport(quiz);
            return quizReport;
        }

        // GET: api/QuizResponses/Questions/5
        [HttpGet("questions/{questionId}")]
        public async Task<ActionResult<IEnumerable<QuizResponse>>> GetQuizResponsesForQuestion(Guid questionId)
        {
            return await _context.QuizResponses
                .Where(e => e.QuestionId == questionId)
                .ToListAsync();
        }
        
        // GET: api/QuizResponses/Users/5
        [HttpGet("users/{userId}")]
        public async Task<ActionResult<IEnumerable<QuizResponse>>> GetQuizResponsesForUser(Guid userId)
        {
            return await _context.QuizResponses
                .Where(e => e.AppUserId == userId)
                .ToListAsync();
        }

        // GET: api/QuizResponses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResponse>> GetQuizResponse(Guid id)
        {
            var quizResponse = await _context.QuizResponses.FindAsync(id);

            if (quizResponse == null)
            {
                return NotFound();
            }

            return quizResponse;
        }

        // PUT: api/QuizResponses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizResponse(Guid id, QuizResponse quizResponse)
        {
            if (id != quizResponse.Id)
            {
                return BadRequest();
            }
            // Can only change your own responses
            if (quizResponse.AppUserId != User.UserGuidId())
            {
                return BadRequest();
            }
            
            _context.Entry(quizResponse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizResponseExists(id))
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

        // POST: api/QuizResponses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<QuizResponse>> PostQuizResponse(QuizResponsesDTO quizResponses)
        {
            foreach (var quizResponse in quizResponses.QuizResponses)
            {
                // Can only respond for yourself
                if (quizResponse.AppUserId != User.UserGuidId())
                {
                    return BadRequest();
                }
                
                await _context.QuizResponses.AddAsync(quizResponse);
            }
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetQuizResponse", new {}, quizResponses.QuizResponses);
        }

        // DELETE: api/QuizResponses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizResponse>> DeleteQuizResponse(Guid id)
        {
            var quizResponse = await _context.QuizResponses.FindAsync(id);
            if (quizResponse == null)
            {
                return NotFound();
            }
            // Can only change your own responses
            if (quizResponse.AppUserId != User.UserGuidId())
            {
                return BadRequest();
            }
            _context.QuizResponses.Remove(quizResponse);
            await _context.SaveChangesAsync();

            return quizResponse;
        }

        private bool QuizResponseExists(Guid id)
        {
            return _context.QuizResponses.Any(e => e.Id == id);
        }
        
         /** Create a report showing how many people responded to which answers (and on which questions) under given quiz plus other data */
        private async Task<QuizReportDTO> GetQuizReport(Quiz quiz)
        {
            var questions = await _context.Questions
                .Where(q => q.QuizId == quiz.Id)
                .Include(q => q.Answers)
                .ThenInclude(a => a.QuizResponses)
                .ToListAsync();
            
            var quizReport = new QuizReportDTO(quiz.Name, new List<ReportQuestionsDTO>());
            foreach (var question in questions)
            {
                // Add question data
                var reportQuestions = new ReportQuestionsDTO(question.Name, new List<ReportAnswersDTO>());
                quizReport.ReportQuestions.Add(reportQuestions);
                
                var answers = question?.Answers;
                if (answers == null)
                {
                    continue;
                }
                foreach (var answer in answers)
                {
                    // Add answer data
                    var reportAnswers = new ReportAnswersDTO(answer.Name, 0);
                    reportQuestions.ReportAnswers.Add(reportAnswers);
                    
                    if (answer.QuizResponses == null)
                    {
                        continue;
                    }

                    var isTypeQuiz = quiz.QuizTypeId == new Guid("00000000-0000-0000-0000-000000000001");
                    if (isTypeQuiz)
                    {
                        reportAnswers.IsCorrect = answer.IsCorrect;
                    }
                    // Add answer counts
                    reportAnswers.ResponseCount = answer.QuizResponses.Count;
                    quizReport.TotalResponseCount += answer.QuizResponses.Count;
                }
                // Sort answers by counts
                reportQuestions.ReportAnswers = reportQuestions.ReportAnswers
                    .OrderByDescending(ra => ra.ResponseCount)
                    .ToList();
            }
            return quizReport;
        }
    }
}
