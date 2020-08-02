﻿using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ExampleBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] public string? Description { get; set; }
    }
}