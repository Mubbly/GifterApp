using System;
using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

 namespace Domain
{
    public class Permission : Permission<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Permissions restrict or allow users to do certain things within the app
     */
    public class Permission<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(1024)] [MinLength(1)] 
        public virtual string PermissionValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }
        
        // List of all users that correspond to this permission
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
    }
}