using IACG.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Test.Base.Data
{
    public static class Helper
    {
        public static async Task<ApplicationDbContext> CreateSampleDbContext(string name)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("InMemoryDb");
            var db = new ApplicationDbContext(optionsBuilder.Options);
            db.Users.RemoveRange(db.Users);
            db.Apps.RemoveRange(db.Apps);
            db.Reviews.RemoveRange(db.Reviews);
            db.Grades.RemoveRange(db.Grades);
            db.Users.Add(new ApplicationUser
            {
                UserName = "admin",
                PasswordHash = "admin"
            });
            await db.SaveChangesAsync();
            return db;
        }

        public static async Task<ApplicationUser> GetSampleAdminUser(ApplicationDbContext db)
        {
            return await db.Users.SingleAsync(u => u.UserName == "admin");
        }
    }
}
