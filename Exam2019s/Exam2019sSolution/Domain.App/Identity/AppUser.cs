using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    //[Table("AspNetUsers")]
    public class AppUser : AppUser<Guid>, IDomainEntityId
    {
        
    }

    public class AppUser<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        // Custom fields
        [MaxLength(256)] [MinLength(1)] [Required]
        public string FirstName { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] [Required]
        public string LastName { get; set; } = default!;
        
        public DateTime DateJoined { get; set; } = DateTime.Now;
        
        public string FullName => FirstName + " " + LastName;

        // Quizzes that this user has created
        [InverseProperty(nameof(Quiz.AppUser))]
        public virtual ICollection<Quiz>? Quizzes { get; set; }
        
        // Responses that this user has given to quizzes/polls
        [InverseProperty(nameof(QuizResponse.AppUser))]
        public virtual ICollection<QuizResponse>? QuizResponses { get; set; }
    }
}