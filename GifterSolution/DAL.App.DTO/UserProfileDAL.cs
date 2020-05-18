﻿using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserProfileDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        public Guid ProfileId { get; set; }
        public ProfileDAL Profile { get; set; } = default!;
    }
}