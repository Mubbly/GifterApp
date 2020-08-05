using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ExampleDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] public string? Description { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUserDAL? AppUser { get; set; } = default!;
    }
}