using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class ExampleDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] public string? Description { get; set; }
        
        public Guid? AppUserId { get; set; }
        public AppUserDTO? AppUser { get; set; } = default!;
    }
}