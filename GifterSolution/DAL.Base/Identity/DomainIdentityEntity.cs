using System;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace DAL.Base.Identity
{
    public abstract class DomainIdentityEntity : IdentityUser<Guid>, IDomainEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? EditedBy { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}