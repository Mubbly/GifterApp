using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityMetadata : IDomainEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
        
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}