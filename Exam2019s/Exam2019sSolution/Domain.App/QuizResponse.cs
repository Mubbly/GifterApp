using System;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class QuizResponse : QuizResponse<Guid>
    {
        
    }
    
    public class QuizResponse<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey: struct, IEquatable<TKey>
    {
        // Answer that was given
        public virtual TKey AnswerId { get; set; } = default!;
        public virtual Answer? Answer { get; set; }
        
        // Question that the answer/response belongs to
        public virtual TKey QuestionId { get; set; } = default!;
        public virtual Question? Question { get; set; }
        
        // Quiz that the question/response belongs to
        public virtual TKey QuizId { get; set; } = default!;
        public virtual Quiz? Quiz { get; set; }

        // AppUser that gave the answer
    }
}