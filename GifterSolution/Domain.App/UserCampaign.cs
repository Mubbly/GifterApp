using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserCampaign : UserCampaign<Guid>
    {
    }

    public class UserCampaign<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // AppUser

        public virtual TKey CampaignId { get; set; } = default!;
        public virtual Campaign? Campaign { get; set; }
    }
}