using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ActionTypeDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] 
        public string ActionTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public int GiftsCount { get; set; }
        public int ReservedGiftsCount { get; set; }
        public int ArchivedGiftsCount { get; set; }
        public int DonateesCount { get; set; }
    }
}