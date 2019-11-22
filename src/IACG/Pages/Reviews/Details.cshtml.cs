using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IACG.Data;

namespace IACG.Pages.Reviews
{
    public class DetailsModel : PageModel
    {
        private readonly IACG.Data.ApplicationDbContext _context;

        public DetailsModel(IACG.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews
                .Include(r => r.User).Include(r => r.App).FirstOrDefaultAsync(m => m.Id == id);

            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
