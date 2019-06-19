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
                var apps = await client.Apps.GetAllAsync();
                Assert.IsAssignableFrom<IEnumerable<LuisApp>>(apps);
            }
        }

        [Fact]
        public async Task ShouldGetExistAppById()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var apps = await client.Apps.GetAllAsync();

                var firstApp = apps.FirstOrDefault();

                var app = await client.Apps.GetByIdAsync(firstApp.Id);
                Assert.Equal(firstApp.Name, app.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsAppId()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.Apps.GetByIdAsync(InvalidId);
                Assert.Null(app);
            }
        }

        [Fact]
        public async Task ShouldGetAppByName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Apps.GetByNameAsync("SDKTest") == null)
                    await client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);

                var app = await client.Apps.GetByNameAsync("SDKTest");
                Assert.NotNull(app);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsAppName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var appTest = await client.Apps.GetByNameAsync("SDKTest");
                if (appTest != null)
                    await client.Apps.DeleteAsync(appTest.Id);

                var app = await client.Apps.GetByNameAsync("SDKTest");
                Assert.Null(app);
            }
        }

        [Fact]
        public async Task ShouldAddNewAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var appTest = await client.Apps.GetByNameAsync("SDKTest");
                if (appTest != null)
                    await client.Apps.DeleteAsync(appTest.Id);

                var newId = await client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddNewAppSDKTestWhenAlreadyExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion));

                Assert.Equal("BadArgument - SDKTest already exists.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.Apps.GetByNameAsync("SDKTest");
                var appChanged = await client.Apps.GetByNameAsync("SDKTestChanged");

                if (app == null)
                {
                    await client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                    app = await client.Apps.GetByNameAsync("SDKTest");
                }

                if (appChanged != null)
                    await client.Apps.DeleteAsync(appChanged.Id);

                await client.Apps.RenameAsync(app.Id, "SDKTestChanged", "Description changed");

                app = await client.Apps.GetByIdAsync(app.Id);
                Assert.Equal("SDKTestChanged", app.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenExistsAppWithSameName()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var app = await client.Apps.GetByNameAsync("SDKTest");
                var appChanged = await client.Apps.GetByNameAsync("SDKTestChanged");
                string appChangedId = null;

                if (app == null)
                {
                    await client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
                    app = await client.Apps.GetByNameAsync("SDKTest");
                }
                if (appChanged == null)
                    appChangedId = await client.Apps.AddAsync("SDKTestChanged", "Description changed", "en-us", "SDKTest", string.Empty, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Apps.RenameAsync(app.Id, "SDKTestChanged", "Description changed"));

                Assert.Equal("BadArgument - SDKTestChanged already exists.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenNotExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Apps.RenameAsync(InvalidId, "SDKTest", "SDKTestChanged"));

                Assert.Equal("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteAppSDKTest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Apps.GetByNameAsync("SDKTest") == null)
                    await client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);

                var app = await client.Apps.GetByNameAsync("SDKTest");
                await client.Apps.DeleteAsync(app.Id);
                var newapp = await client.Apps.GetByIdAsync(app.Id);

                Assert.Null(newapp);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteAppSDKTestWhenNotExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Apps.DeleteAsync(InvalidId));

                Assert.Equal("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
