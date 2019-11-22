using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IACG.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IACG.Pages.Admin
{
    [Authorize(Roles = nameof(UserRoles.Administrator))]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostInitializeAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await SeedData.InitializeApps(_context);
            await SeedData.InitializeReviews(_context);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Reviews.RemoveRange(_context.Reviews);
            _context.Grades.RemoveRange(_context.Grades);
            _context.Apps.RemoveRange(_context.Apps);

            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
