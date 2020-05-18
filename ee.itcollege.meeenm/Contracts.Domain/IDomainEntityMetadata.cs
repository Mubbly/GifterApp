using System;

namespace com.mubbly.gifterapp.Contracts.Domain
{
    public interface IDomainEntityMetadata
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}