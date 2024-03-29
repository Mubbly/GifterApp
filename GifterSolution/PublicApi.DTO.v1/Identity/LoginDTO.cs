﻿using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class LoginDTO
    {
        [MaxLength(256)]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = default!;

        [MinLength(8)]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; } = default!;
    }
}