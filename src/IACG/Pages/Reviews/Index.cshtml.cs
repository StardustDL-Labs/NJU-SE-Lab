using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IACG.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IACG.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context; 
        private readonly UserManager<ApplicationUser> _userManager;

        public class PostModel
        {
            public int? QueryAppId { get; set; }

            public ReviewResult? QueryResult { get; set; }
        }

        [BindProperty]
        public PostModel PostData { get; set; }

        public IndexModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Review> Review { get;set; }

        IQueryable<Review> GetInitial()
        {
            return from a in _context.Reviews.Include(a => a.User)
                   where a.UserId == _userManager.GetUserId(User)
                   select a;
        }

        public async Task OnGetAsync()
        {
            Review = await GetInitial().ToListAsync();
        }

        public async Task<IActionResult> OnPostQueryAsync()
        {
            try
            {
                var query = GetInitial();
                if (PostData.QueryAppId != null)
                {
                    query = query.Where(a => a.AppId==PostData.QueryAppId);
                }
                if(PostData.QueryResult != null)
                {
                    query = query.Where(a => a.Result == PostData.QueryResult);
                }
                Review = await query.ToListAsync();
            }
            catch
            {
                Review = Array.Empty<Review>();
            }
            return Page();
        }
    }
}
