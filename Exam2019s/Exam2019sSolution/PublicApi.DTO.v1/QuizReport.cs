using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class QuizReportDTO
    {
        public QuizReportDTO()
        {
        }
        
        public QuizReportDTO(string quizName, ICollection<ReportQuestionsDTO> reportQuestions)
        {
            Name = quizName;
            ReportQuestions = reportQuestions;
        }
        
        [Required]
        public string Name { get; set; } = default!;
        
        [Required] public int TotalResponseCount { get; set; }
        
        [Required] 
        public ICollection<ReportQuestionsDTO> ReportQuestions { get; set; } = new List<ReportQuestionsDTO>();
    }
    
    public class ReportQuestionsDTO
    {
        public ReportQuestionsDTO()
        {
        }
        
        public ReportQuestionsDTO(string questionName, ICollection<ReportAnswersDTO> reportAnswers)
        {
            Name = questionName;
            ReportAnswers = reportAnswers;
        }
        
        [Required] public string Name { get; set; } = default!;
        public ICollection<ReportAnswersDTO> ReportAnswers { get; set; } = new List<ReportAnswersDTO>();
    }

    public class ReportAnswersDTO
    {
        public ReportAnswersDTO()
        {
        }
        
        public ReportAnswersDTO(string answerName, int responseCount)
        {
            Name = answerName;
            ResponseCount = responseCount;
        }
        
        [Required] public string Name { get; set; } = default!;

        [Required] public bool? IsCorrect { get; set; }
        [Required] public int ResponseCount { get; set; }
    }
}