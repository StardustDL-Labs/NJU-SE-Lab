using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IACG.Data;
using Microsoft.AspNetCore.Identity;

namespace IACG.Pages.Reviews
{
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
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews
                .Include(r => r.User).Include(r => r.App).FirstOrDefaultAsync(m => m.Id == id);

            if (Review == null || Review.UserId != _userManager.GetUserId(User))
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

            var review = await _context.Reviews.FindAsync(id);

            if (review == null || review.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            review.Result = Review.Result;
            review.Comment = Review.Comment;
            review.LastModifyTime = DateTimeOffset.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
