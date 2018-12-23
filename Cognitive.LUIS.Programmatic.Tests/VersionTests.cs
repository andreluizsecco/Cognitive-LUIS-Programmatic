using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class VersionTests : BaseTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context) =>
            Initialize();

        [ClassCleanup]
        public static void ClassCleanup() =>
            Cleanup();

        [TestMethod]
        public async Task ShouldGetVersionList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.GetAppByNameAsync("SDKTest");

                // Act
                var versions = await client.GetAllVersionsAsync(app.Id);

                Assert.IsInstanceOfType(versions, typeof(IEnumerable<AppVersion>));
            }
        }

        [TestMethod]
        public async Task ShouldGetEmptyEnumerableWhenAppIdDoesNotExist()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Act
                var versions = await client.GetAllVersionsAsync(appId: InvalidId);

                Assert.IsNotNull(versions);
                Assert.IsTrue(versions.Count == 0, "Method should return an empty enumerable.");
            }
        }

        [TestMethod]
        public async Task ShouldGetNullWhenAppIdDoesNotExist()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Act
                var version = await client.GetVersionAsync(InvalidId, "1.0");

                Assert.IsNull(version);
            }
        }

        [TestMethod]
        public async Task ShouldGetVersion()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.GetAppByNameAsync("SDKTest");

                // Act
                var version = await client.GetVersionAsync(app.Id, "1.0");

                Assert.IsNotNull(version);
            }
        }
    }
}
