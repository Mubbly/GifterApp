using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class QuizType : QuizType<Guid>
    {
        
    }
    
    public class QuizType<TKey> : DomainEntityIdMetadata
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;
        
        // Quizzes that are of this type
        [InverseProperty(nameof(Quiz.QuizType))]
        public virtual ICollection<Quiz>? Quizzes { get; set; }
    }
}