using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1.Identity
{
    public class RegisterDTO : LoginDTO
    {
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; } = default!;
    }
}