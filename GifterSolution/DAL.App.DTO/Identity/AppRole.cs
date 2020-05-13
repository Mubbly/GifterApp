using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppRole : IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = default!;

        public Guid Id { get; set; }
    }
}