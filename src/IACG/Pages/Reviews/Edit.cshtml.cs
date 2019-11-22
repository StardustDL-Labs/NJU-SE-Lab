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
using IACG.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace IACG.Pages.Reviews
{
    public class EditModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.App).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Review == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, Review, ModelOperations.Update);
            if (authorizationResult.Succeeded)
            {
                return Page();
            }
            else
            {
                return Forbid();
            }
        }

        async Task<IActionResult> Posting(int? id, ReviewResult result)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, review, ModelOperations.Update);
            if (authorizationResult.Succeeded)
            {
                review.Result = result;
                review.Comment = Review.Comment;
                review.LastModifyTime = DateTimeOffset.Now;

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
            {
                return Forbid();
            }
        }

        public Task<IActionResult> OnPostWaitingAsync(int? id)
        {
            return Posting(id, ReviewResult.Waiting);
        }

        public Task<IActionResult> OnPostAcceptAsync(int? id)
        {
            return Posting(id, ReviewResult.Accept);
        }

        public Task<IActionResult> OnPostRejectAsync(int? id)
        {
            return Posting(id, ReviewResult.Reject);
        }
    }
}
