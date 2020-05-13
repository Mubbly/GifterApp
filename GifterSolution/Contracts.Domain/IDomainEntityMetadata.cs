using System;

namespace Contracts.Domain
{
    public interface IDomainEntityMetadata
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }

        // No soft update/delete initially
        // public string? DeletedBy { get; set; }
        // public DateTime? DeletedAt { get; set; }
    }
}