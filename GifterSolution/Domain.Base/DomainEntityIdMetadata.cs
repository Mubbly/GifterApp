using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Newtonsoft.Json;

namespace Domain.Base
{
    public abstract class DomainEntityIdMetadata : DomainEntityIdMetadata<Guid>, IDomainEntityId, IDomainEntityMetadata
    {
    }

    public abstract class DomainEntityIdMetadata<TKey> : DomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        [MaxLength(256)] [JsonIgnore] public virtual string? CreatedBy { get; set; }

        [JsonIgnore] public virtual DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(256)] [JsonIgnore] public virtual string? EditedBy { get; set; }

        [JsonIgnore] public virtual DateTime? EditedAt { get; set; } = DateTime.Now;

        // No soft update/delete right now
        // public string? DeletedBy { get; set; }
        // public DateTime? DeletedAt { get; set; }
    }
}