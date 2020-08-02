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


        // Example:
        // List of all notifications that correspond to this user
        // [InverseProperty(nameof(UserNotification.AppUser))]
        // public ICollection<UserNotification>? UserNotifications { get; set; }
    }
}