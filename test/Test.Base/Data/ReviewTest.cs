using IACG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Test.Base.Data
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public async Task Create()
        {
            using (var db = await Helper.CreateSampleDbContext(nameof(Create)))
            {
                var admin = await Helper.GetSampleAdminUser(db);

                App app = new App
                {
                    Name = "app",
                    Description = "des",
                    UserId = admin.Id,
                };

                db.Apps.Add(app);

                await db.SaveChangesAsync();

                db.Reviews.Add(new Review
                {
                    AppId = app.Id,
                    UserId = admin.Id,
                    Comment = "com",
                    Result = ReviewResult.Waiting
                });

                await db.SaveChangesAsync();

                Assert.AreEqual(1, await db.Reviews.CountAsync(a => a.Comment == "com"));
            }
        }

        [TestMethod]
        public async Task Update()
        {
            using (var db = await Helper.CreateSampleDbContext(nameof(Create)))
            {
                var admin = await Helper.GetSampleAdminUser(db);

                App app = new App
                {
                    Name = "app",
                    Description = "des",
                    UserId = admin.Id,
                };

                db.Apps.Add(app);

                await db.SaveChangesAsync();

                Assert.AreEqual(1, await db.Apps.CountAsync(a => a.Name == "app"));

                Review review = new Review
                {
                    AppId = app.Id,
                    UserId = admin.Id,
                    Comment = "com",
                    Result = ReviewResult.Waiting
                };

                db.Reviews.Add(review);

                await db.SaveChangesAsync();

                Assert.AreEqual(1, await db.Reviews.CountAsync(a => a.Comment == "com"));

                review.Result = ReviewResult.Accept;

                await db.SaveChangesAsync();

                Assert.AreEqual(ReviewResult.Accept, (await db.Reviews.FindAsync(review.Id)).Result);
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            using (var db = await Helper.CreateSampleDbContext(nameof(Create)))
            {
                var admin = await Helper.GetSampleAdminUser(db);

                App app = new App
                {
                    Name = "app",
                    Description = "des",
                    UserId = admin.Id,
                };

                db.Apps.Add(app);

                await db.SaveChangesAsync();

                Review review = new Review
                {
                    AppId = app.Id,
                    UserId = admin.Id,
                    Comment = "com",
                    Result = ReviewResult.Waiting
                };

                db.Reviews.Add(review);

                await db.SaveChangesAsync();

                Assert.AreEqual(1, await db.Reviews.CountAsync(a => a.Comment == "com"));

                db.Reviews.Remove(review);

                await db.SaveChangesAsync();

                Assert.AreEqual(0, await db.Reviews.CountAsync(a => a.Comment == "com"));
            }
        }

        [TestMethod]
        public async Task Read()
        {
            using (var db = await Helper.CreateSampleDbContext(nameof(Create)))
            {
                var admin = await Helper.GetSampleAdminUser(db);

                App app = new App
                {
                    Name = "app",
                    Description = "des",
                    UserId = admin.Id,
                };

                db.Apps.Add(app);

                await db.SaveChangesAsync();

                Review review = new Review
                {
                    AppId = app.Id,
                    UserId = admin.Id,
                    Comment = "com",
                    Result = ReviewResult.Waiting
                };

                db.Reviews.Add(review);

                await db.SaveChangesAsync();

                Assert.AreEqual(1, await db.Reviews.CountAsync(a => a.Comment == "com"));

                Assert.AreEqual(ReviewResult.Waiting, (await db.Reviews.SingleAsync(a => a.Comment == "com")).Result);
            }
        }
    }
}
