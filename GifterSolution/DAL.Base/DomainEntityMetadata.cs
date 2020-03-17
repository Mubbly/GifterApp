using System;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntityMetadata : IDomainEntityMetadata
    {
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual string? EditedBy { get; set; }
        public virtual DateTime? EditedAt { get; set; } = DateTime.Now;
        
        // No soft update/delete right now
        // public string? DeletedBy { get; set; }
        // public DateTime? DeletedAt { get; set; }
    }
}