﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace DAL.App.DTO
{
    public class NotificationTypeDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] [MinLength(1)] public string NotificationTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // List of all notifications that correspond to this type
        [InverseProperty(nameof(NotificationDAL.NotificationType))]
        public ICollection<NotificationDAL>? Notifications { get; set; }
    }
}