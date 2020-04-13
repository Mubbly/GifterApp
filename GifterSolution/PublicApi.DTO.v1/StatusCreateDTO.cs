using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class StatusCreateDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] 
        public string StatusValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
    }
}