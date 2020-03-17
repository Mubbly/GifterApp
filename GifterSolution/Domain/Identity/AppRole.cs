using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    [Table("AspNetUsers")]
    public class AppRole : IdentityRole<Guid>
    {
        
    }
}