using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class AppTests : BaseTest
    {
        public AppTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetAppList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var apps = await client.GetAllAppsAsync();
                Assert.IsAssignableFrom<IEnumerable<LuisApp>>(apps);
            }
        }

        [Fact]
        public async Task ShouldGetExistAppById()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var apps = await client.GetAllAppsAsync();

                var firstApp = apps.FirstOrDefault();

                var app = await client.GetAppByIdAsync(firstApp.Id);
                Assert.Equal(firstApp.Name, app.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsAppId()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.GetAppByIdAsync(InvalidId);
                Assert.Null(app);
            }
        }

        [Fact]
        public async Task ShouldGetAppByName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetAppByNameAsync("SDKTest") == null)
                    await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);

                var app = await client.GetAppByNameAsync("SDKTest");
                Assert.NotNull(app);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsAppName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var appTest = await client.GetAppByNameAsync("SDKTest");
                if (appTest != null)
                    await client.DeleteAppAsync(appTest.Id);

                var app = await client.GetAppByNameAsync("SDKTest");
                Assert.Null(app);
            }
        }

        [Fact]
        public async Task ShouldAddNewAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var appTest = await client.GetAppByNameAsync("SDKTest");
                if (appTest != null)
                    await client.DeleteAppAsync(appTest.Id);

                var newId = await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddNewAppSDKTestWhenAlreadyExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion));

                Assert.Equal("BadArgument - SDKTest already exists.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.GetAppByNameAsync("SDKTest");
                var appChanged = await client.GetAppByNameAsync("SDKTestChanged");

                if (app == null)
                {
                    await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                    app = await client.GetAppByNameAsync("SDKTest");
                }

                if (appChanged != null)
                    await client.DeleteAppAsync(appChanged.Id);

                await client.RenameAppAsync(app.Id, "SDKTestChanged", "Description changed");

                app = await client.GetAppByIdAsync(app.Id);
                Assert.Equal("SDKTestChanged", app.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenExistsAppWithSameName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.GetAppByNameAsync("SDKTest");
                var appChanged = await client.GetAppByNameAsync("SDKTestChanged");
                string appChangedId = null;

                if (app == null)
                {
                    await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                    app = await client.GetAppByNameAsync("SDKTest");
                }
                if (appChanged == null)
                    appChangedId = await client.AddAppAsync("SDKTestChanged", "Description changed", "en-us", "SDKTest", string.Empty, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.RenameAppAsync(app.Id, "SDKTestChanged", "Description changed"));

                Assert.Equal("BadArgument - SDKTestChanged already exists.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenNotExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.RenameAppAsync(InvalidId, "SDKTest", "SDKTestChanged"));

                Assert.Equal("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetAppByNameAsync("SDKTest") == null)
                    await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);

                var app = await client.GetAppByNameAsync("SDKTest");
                await client.DeleteAppAsync(app.Id);
                var newapp = await client.GetAppByIdAsync(app.Id);

                Assert.Null(newapp);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteAppSDKTestWhenNotExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.DeleteAppAsync(InvalidId));

                Assert.Equal("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
