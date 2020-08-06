using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;

namespace PublicApi.DTO.v1
{
    public class QuizResponsesDTO
    {
        public QuizResponsesDTO()
        {
        }

        public QuizResponsesDTO(params QuizResponse[] quizResponses)
        {
            QuizResponses = quizResponses;
        }

        [Required] 
        public IList<QuizResponse> QuizResponses { get; set; } = new List<QuizResponse>();
    }
}