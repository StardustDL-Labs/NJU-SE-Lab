using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IACG.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<App> Apps { get; set; }

        public DbSet<AppCategory> AppCategories { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Grade> Grades { get; set; }
    }
}
