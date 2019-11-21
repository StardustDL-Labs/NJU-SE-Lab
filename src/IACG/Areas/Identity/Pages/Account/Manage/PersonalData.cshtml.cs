using System.Threading.Tasks;
using IACG.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IACG.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager) => _userManager = userManager;

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            return user == null ? NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.") : (IActionResult)Page();
        }
    }
}