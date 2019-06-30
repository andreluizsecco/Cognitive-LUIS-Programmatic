using Cognitive.LUIS.Programmatic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class VersionTests : BaseTest
    {
        public VersionTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetVersionList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.Apps.GetByNameAsync("SDKTest");

                // Act
                var versions = await client.Versions.GetAllAsync(app.Id);

                Assert.IsAssignableFrom<IEnumerable<AppVersion>>(versions);
            }
        }

        [Fact]
        public async Task ShouldGetEmptyEnumerableWhenAppIdDoesNotExist()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Act
                var versions = await client.Versions.GetAllAsync(appId: InvalidId);

                Assert.NotNull(versions);
                Assert.True(versions.Count == 0, "Method should return an empty enumerable.");
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenAppIdDoesNotExist()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Act
                var version = await client.Versions.GetByIdAsync(InvalidId, "1.0");

                Assert.Null(version);
            }
        }

        [Fact]
        public async Task ShouldGetVersion()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.Apps.GetByNameAsync("SDKTest");

                // Act
                var version = await client.Versions.GetByIdAsync(app.Id, "1.0");

                Assert.NotNull(version);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
