using IACG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Base.Data
{
    [TestClass]
    public class AppTest
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

                Assert.AreEqual(1, await db.Apps.CountAsync(a => a.Name == "app"));
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

                app.Description = "description";

                await db.SaveChangesAsync();

                Assert.AreEqual("description", (await db.Apps.SingleAsync(a => a.Name == "app")).Description);
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

                Assert.AreEqual(1, await db.Apps.CountAsync(a => a.Name == "app"));

                db.Apps.Remove(app);

                await db.SaveChangesAsync();

                Assert.AreEqual(0, await db.Apps.CountAsync(a => a.Name == "app"));
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

                Assert.AreEqual(1, await db.Apps.CountAsync(a => a.Name == "app"));

                Assert.AreEqual("des", (await db.Apps.SingleAsync(a => a.Name == "app")).Description);
            }
        }
    }
}
