using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserCampaign : UserCampaign<Guid>, IDomainEntity
    {
        
    }
    public class UserCampaign<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }
        
        public virtual TKey CampaignId { get; set; } = default!;
        public virtual Campaign? Campaign { get; set; }
    }
}