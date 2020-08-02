using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUserDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;

        // Custom fields
        [MaxLength(256)] [MinLength(1)] [Required]
        public string FirstName { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] [Required]
        public string LastName { get; set; } = default!;
        
        public DateTime DateJoined { get; set; } = DateTime.Now;
        
        public string FullName => FirstName + " " + LastName;
    }
}