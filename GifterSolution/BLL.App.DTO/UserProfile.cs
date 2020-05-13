using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserProfile : IDomainEntityId
    {
        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; } = default!;
        public Guid Id { get; set; }
    }
}