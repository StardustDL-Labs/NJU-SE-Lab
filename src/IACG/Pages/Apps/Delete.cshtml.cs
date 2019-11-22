using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IACG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IACG.Helpers;

namespace IACG.Pages.Apps
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public App App { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            App = await _context.Apps
                .Include(a => a.User).FirstOrDefaultAsync(m => m.Id == id);

            if (App == null)
            {
                return NotFound();
            }
            else
            {
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, App, ModelOperations.Delete);
                if (authorizationResult.Succeeded)
                {
                    return Page();
                }
                else
                {
                    return Forbid();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            App = await _context.Apps.FindAsync(id);

            if (App == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, App, ModelOperations.Delete);
            if(authorizationResult.Succeeded)
            {
                _context.Apps.Remove(App);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            else
            {
                return Forbid();
            }
        }
    }
}
