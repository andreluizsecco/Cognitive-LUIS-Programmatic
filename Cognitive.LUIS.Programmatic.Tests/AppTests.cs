using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class AppTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private const Location LOCATION = Location.WestUS;

        [TestMethod]
        public async Task ShouldGetAppList()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var apps = await client.GetAllAppsAsync();
            Assert.IsInstanceOfType(apps, typeof(IEnumerable<LuisApp>));
        }

        [TestMethod]
        public async Task ShouldGetExistAppById()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var apps = await client.GetAllAppsAsync();

            var firstApp = apps.FirstOrDefault();

            var app = await client.GetAppByIdAsync(firstApp.Id);
            Assert.AreEqual(firstApp.Name, app.Name);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsAppId()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);

            var app = await client.GetAppByIdAsync("51593248-363e-4a08-b946-2061964dc690");
            Assert.IsNull(app);
        }

        [TestMethod]
        public async Task ShouldGetAppByName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            if (await client.GetAppByNameAsync("SDKTest") == null)
                await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0");

            var app = await client.GetAppByNameAsync("SDKTest");
            Assert.IsNotNull(app);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsAppName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var appTest = await client.GetAppByNameAsync("SDKTest");
            if (appTest != null)
                await client.DeleteAppAsync(appTest.Id);

            var app = await client.GetAppByNameAsync("SDKTest");
            Assert.IsNull(app);
        }

        [TestMethod]
        public async Task ShouldAddNewAppSDKTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);

            var appTest = await client.GetAppByNameAsync("SDKTest");
            if (appTest != null)
                await client.DeleteAppAsync(appTest.Id);

            var newId = await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0");
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnAddNewAppSDKTestWhenAlreadyExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0"));

            Assert.AreEqual(ex.Message, "An application with the same name already exists");
        }

        [TestMethod]
        public async Task ShouldRenameAppSDKTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var app = await client.GetAppByNameAsync("SDKTest");
            var appChanged = await client.GetAppByNameAsync("SDKTestChanged");

            if (app == null)
            {
                await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0");
                app = await client.GetAppByNameAsync("SDKTest");
            }

            if (appChanged != null)
                await client.DeleteAppAsync(appChanged.Id);
            
            await client.RenameAppAsync(app.Id, "SDKTestChanged", "Description changed");

            app = await client.GetAppByIdAsync(app.Id);
            Assert.AreEqual("SDKTestChanged", app.Name);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenExistsAppWithSameName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var app = await client.GetAppByNameAsync("SDKTest");
            var appChanged = await client.GetAppByNameAsync("SDKTestChanged");
            string appChangedId = null;

            if (app == null)
            {
                await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0");
                app = await client.GetAppByNameAsync("SDKTest");
            }
            if (appChanged == null)
                appChangedId = await client.AddAppAsync("SDKTestChanged", "Description changed", "en-us", "SDKTest", string.Empty, "1.0");

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameAppAsync(app.Id, "SDKTestChanged", "Description changed"));

            Assert.AreEqual(ex.Message, "An application with the same name already exists");
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameAppSDKTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameAppAsync("51593248-363e-4a08-b946-2061964dc690", "SDKTest", "SDKTestChanged"));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }

        [TestMethod]
        public async Task ShouldDeleteAppSDKTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            if (await client.GetAppByNameAsync("SDKTest") == null)
                await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0");

            var app = await client.GetAppByNameAsync("SDKTest");
            await client.DeleteAppAsync(app.Id);
            app = await client.GetAppByIdAsync(app.Id);

            Assert.IsNull(app);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnDeleteAppSDKTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => 
                client.DeleteAppAsync("51593248-363e-4a08-b946-2061964dc690"));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }
    }
}
