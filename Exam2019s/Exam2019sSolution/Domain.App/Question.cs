using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class Question : Question<Guid>
    {
        
    }
    
    public class Question<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;
        
        // Quiz that the question belongs to
        public virtual TKey QuizId { get; set; } = default!;
        public virtual Quiz? Quiz { get; set; }
        
        // Answer choices that the question has
        [InverseProperty(nameof(Answer.Question))]
        public virtual ICollection<Answer>? Answers { get; set; }
        
        // Answer choices that the question has
        [InverseProperty(nameof(QuizResponse.Question))]
        public virtual ICollection<QuizResponse>? QuizResponses { get; set; }
    }
}