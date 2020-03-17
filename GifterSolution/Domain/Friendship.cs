using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Users can add other users to their friendlist
     * Friends have more permissions (ex. view profile when it's private, send private messages) 
     */
    public class Friendship : DomainEntity
    {
        public bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        // Requester
        [ForeignKey(nameof(AppUser1))]
        public Guid AppUser1Id { get; set; } = default!;
        public AppUser? AppUser1 { get; set; }
        
        // Addressee
        [ForeignKey(nameof(AppUser2))]
        public Guid AppUser2Id { get; set; } = default!;
        public AppUser? AppUser2 { get; set; }
    }
}