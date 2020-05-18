using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

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