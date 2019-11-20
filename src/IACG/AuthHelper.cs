using IACG.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IACG
{
    public static class AuthHelper
    {
        public static async Task<string> TryGetUserEmail(ClaimsPrincipal user, UserManager<ApplicationUser> manager)
        {
            try
            {
                ApplicationUser ru = await manager.GetUserAsync(user);
                return await manager.GetEmailAsync(ru);
            }
            catch
            {
                return "";
            }
        }
    }
}
