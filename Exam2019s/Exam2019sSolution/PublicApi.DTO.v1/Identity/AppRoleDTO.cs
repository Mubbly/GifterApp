using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace PublicApi.DTO.v1.Identity
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