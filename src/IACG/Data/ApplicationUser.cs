using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IACG.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
