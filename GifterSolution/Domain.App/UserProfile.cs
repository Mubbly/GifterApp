using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class UserProfile : UserProfile<Guid>
    {
    }

    public class UserProfile<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // AppUser

        public virtual TKey ProfileId { get; set; } = default!;
        public virtual Profile? Profile { get; set; }
    }
}