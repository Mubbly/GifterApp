using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class Answer : Answer<Guid>
    {
        
    }
    
    public class Answer<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;
        
        public virtual bool IsCorrect { get; set; } = default!;
        
        // Question that the answer belongs to
        public virtual TKey QuestionId { get; set; } = default!;
        public virtual Question? Question { get; set; }
        
        // Responses, showing who has selected this answer
        [InverseProperty(nameof(QuizResponse.Answer))]
        public virtual ICollection<QuizResponse>? QuizResponses { get; set; }
    }
}