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

        public class BindModel
        {
            public int? QueryAppId { get; set; }

            public ReviewResult? QueryResult { get; set; }
        }

        [BindProperty]
        public BindModel BindData { get; set; }

        public IndexModel(IACG.Data.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Review> Review { get; set; }

        public async Task OnGetAsync(int? appId = null, ReviewResult? result = null)
        {
            ViewData["SelectAppId"] = Enumerable.Empty<SelectListItem>().Append(new SelectListItem("全部", "null")).Concat(_context.Apps.Select(a => new SelectListItem(a.Name, a.Id.ToString()))).ToList();

            var query = from a in _context.Reviews.Include(a => a.User).Include(a => a.App)
                        where a.UserId == _userManager.GetUserId(User)
                        select a;
            if (appId != null)
            {
                query = query.Where(a => a.AppId == appId);
            }
            if (result != null)
            {
                query = query.Where(a => a.Result == result);
            }
            Review = await query.ToListAsync();

            BindData = new BindModel
            {
                QueryAppId = appId,
                QueryResult = result
            };
        }

        public IActionResult OnPostQuery()
        {
            return RedirectToRoute(new { appId = BindData.QueryAppId, result = BindData.QueryResult });
        }
    }
}
