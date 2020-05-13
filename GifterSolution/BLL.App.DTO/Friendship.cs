using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class Friendship : IDomainEntityId
    {
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // Requester
        public Guid AppUser1Id { get; set; }

        public AppUser AppUser1 { get; set; } = default!;

        // Addressee
        public Guid AppUser2Id { get; set; }
        public AppUser AppUser2 { get; set; } = default!;
        public Guid Id { get; set; }
    }
}