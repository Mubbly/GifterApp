using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    //[Table("AspNetRoles")]
    public class AppRole : AppRole<Guid>
    {
    }

    public class AppRole<TKey> : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {

    }
}