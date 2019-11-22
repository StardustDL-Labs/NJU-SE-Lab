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
    public class ReviewModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ReviewModel(IACG.Data.ApplicationDbContext context,
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
                return BadRequest();
            }

            App = await _context.Apps
                .Include(a => a.User).FirstOrDefaultAsync(m => m.Id == id);

            if (App == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.AppId == App.Id && r.UserId == _userManager.GetUserId(User));

            if (review != null)
            {
                return RedirectToPage("/Reviews/Details", new { id = review.Id });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            App = await _context.Apps.FindAsync(id);

            if (App == null)
            {
                return NotFound();
            }

            if(await _context.Reviews.AnyAsync(r => r.AppId == App.Id && r.UserId == _userManager.GetUserId(User)))
            {
                return BadRequest();
            }

            Review review = new Review
            {
                AppId = App.Id,
                UserId = _userManager.GetUserId(User),
                LastModifyTime = DateTimeOffset.Now,
                Result = ReviewResult.Waiting,
                Comment = ""
            };

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, App, ModelAppOperations.Review);
            if (authorizationResult.Succeeded)
            {
                authorizationResult = await _authorizationService.AuthorizeAsync(User, review, ModelOperations.Create);
                if (authorizationResult.Succeeded)
                {
                    _context.Reviews.Add(review);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("/Reviews/Details", new { id = review.Id });
                }
                else
                {
                    return Forbid();
                }
            }
            else
            {
                return Forbid();
            }
        }
    }
}
