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
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public class BindModel
        {
            public string QueryName { get; set; }
        }

        [BindProperty]
        public BindModel BindData { get; set; } = new BindModel();

        public IndexModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<App> App { get; set; }

        public async Task OnGetAsync(string name = null)
        {
            var query = from a in _context.Apps.Include(a => a.User)
                        select a;

            if (User.IsInRole(nameof(UserRoles.Enterprise)))
            {
                query = query.Where(a => a.UserId == _userManager.GetUserId(User));
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            App = await query.ToListAsync();

            BindData = new BindModel
            {
                QueryName = name
            };
        }

        public IActionResult OnPostQuery()
        {
            return RedirectToRoute(new { name = BindData.QueryName });
        }
    }
}
