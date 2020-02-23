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
    public class Friendship : DomainEntityMetadata
    {
        public bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        // TODO: Manual connections! (Where two users)
        [ForeignKey(nameof(AppUser1))] [MaxLength(36)]
        public string AppUser1Id { get; set; } = default!;
        public AppUser? AppUser1 { get; set; }
        
        [ForeignKey(nameof(AppUser2))] [MaxLength(36)]
        public string AppUser2Id { get; set; } = default!;
        public AppUser? AppUser2 { get; set; }
    }
}