﻿using System;
using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using DAL.Base;

 namespace Domain
{
    /**
     * Temporary table - will actually preexist from Identity?
     * Permissions restrict or allow users to do certain things within the app
     */
    public class Permission : DomainEntityMetadata
    {
        [MaxLength(1024)] [MinLength(1)] 
        public string PermissionValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        // List of all users that correspond to this permission
        public ICollection<UserPermission>? UserPermissions { get; set; }
    }
}