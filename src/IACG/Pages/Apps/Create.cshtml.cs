using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using IACG.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IACG.Pages.Apps
{
    [Authorize(Roles = nameof(UserRoles.Enterprise))]
    public class CreateModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IACG.Data.ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public App App { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var app = new App()
            {
                Name = App.Name,
                Description = App.Description,
                UserId = _userManager.GetUserId(User),
                LastModifyTime = DateTimeOffset.Now
            };

            _context.Apps.Add(app);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
