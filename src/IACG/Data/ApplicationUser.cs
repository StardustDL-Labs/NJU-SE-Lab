using Microsoft.AspNetCore.Identity;

namespace IACG.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
