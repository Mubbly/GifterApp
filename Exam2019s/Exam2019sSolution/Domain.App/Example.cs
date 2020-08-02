using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class Example : Example<Guid>
    {
    }
    
    public class Example<TKey> : DomainEntityIdMetadataUser<AppUser> // DomainEntityIdMetadata without user
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public virtual string? Description { get; set; }
    }
}