using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class FriendshipDTO
    {
        public Guid Id { get; set; }
        
        public bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        // Requester
        public Guid AppUser1Id { get; set; }
        // Addressee
        public Guid AppUser2Id { get; set; }
    }
}