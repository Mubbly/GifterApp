using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class Quiz : Quiz<Guid>
    {
        
    }

    public class Quiz<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public virtual string? Description { get; set; }
        
        // public virtual bool IsActive { get; set; }

        // What type of quiz this is
        public virtual TKey QuizTypeId { get; set; } = default!;
        public virtual QuizType? QuizType { get; set; }
        
        // Questions that the quiz contains
        [InverseProperty(nameof(Question.Quiz))]
        public virtual ICollection<Question>? Questions { get; set; }
        
        // Responses that are related to this quiz
        [InverseProperty(nameof(QuizResponse.Quiz))]
        public virtual ICollection<QuizResponse>? QuizResponses { get; set; }
        
        // AppUser that created the quiz
    }
}