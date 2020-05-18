using System;
using Microsoft.AspNetCore.Identity;

namespace com.mubbly.gifterapp.Contracts.Domain
{
    public interface IDomainEntityUser<TUser> : IDomainEntityUser<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    public interface IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey AppUserId { get; set; }
        public TUser? AppUser { get; set; }
    }
}