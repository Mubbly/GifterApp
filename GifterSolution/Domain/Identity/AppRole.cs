using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    [Table("AspNetRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        
    }
}