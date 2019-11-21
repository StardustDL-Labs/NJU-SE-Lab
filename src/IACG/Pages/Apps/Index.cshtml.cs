using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IACG.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IACG.Pages.Apps
{
    [Authorize(Roles = nameof(UserRoles.Enterprise))]
    public class IndexModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public class PostModel
        {
            public string QueryName { get; set; }
        }

        [BindProperty]
        public PostModel PostData { get; set; }

        public IndexModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<App> App { get; set; }

        IQueryable<App> GetInitial()
        {
            return from a in _context.Apps.Include(a => a.User)
                   where a.UserId == _userManager.GetUserId(User)
                   select a;
        }

        public async Task OnGetAsync()
        {
            App = await GetInitial().ToListAsync();
        }

        public async Task<IActionResult> OnPostQueryAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(PostData.QueryName))
                {
                    App = await GetInitial().ToListAsync();
                }
                else
                {
                    App = await (from a in GetInitial() where a.Name.Contains(PostData.QueryName) select a).ToListAsync();
                }
            }
            catch
            {
                App = Array.Empty<App>();
            }
            return Page();
        }
    }
}
