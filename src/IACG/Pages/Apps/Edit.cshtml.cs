using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IACG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IACG.Pages.Apps
{
    [Authorize(Roles = nameof(UserRoles.Enterprise))]
    public class EditModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            if (App == null || App.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var app = await _context.Apps.FindAsync(id);

            if (app == null || app.UserId != _userManager.GetUserId(User))
            {
                return NotFound(); 
            }

            app.Name = App.Name;
            app.Description = App.Description;
            app.LastModifyTime = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool AppExists(int id)
        {
            return _context.Apps.Any(e => e.Id == id);
        }
    }
}
