using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class IntentTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private readonly string _appId;

        public IntentTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                _appId = app.Id;
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldGetIntentList()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intents = await client.GetAllIntentsAsync(_appId, "1.0");
            Assert.IsInstanceOfType(intents, typeof(IEnumerable<Intent>));
        }

        [TestMethod]
        public async Task ShouldGetExistIntentById()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intents = await client.GetAllIntentsAsync(_appId, "1.0");

            var firstIntent = intents.FirstOrDefault();

            var intent = await client.GetIntentByIdAsync(firstIntent.Id, _appId, "1.0");
            Assert.AreEqual(firstIntent.Name, intent.Name);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsIntentId()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);

            var intent = await client.GetIntentByIdAsync("51593248-363e-4a08-b946-2061964dc690", _appId, "1.0");
            Assert.IsNull(intent);
        }

        [TestMethod]
        public async Task ShouldGetIntentByName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            if (await client.GetIntentByNameAsync("IntentTest", _appId, "1.0") == null)
                await client.AddIntentAsync("IntentTest", _appId, "1.0");

            var intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            Assert.IsNotNull(intent);
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsIntentName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                await client.DeleteIntentAsync(intentTest.Id, _appId, "1.0");

            var intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            Assert.IsNull(intent);
        }

        [TestMethod]
        public async Task ShouldAddNewIntentTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);

            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                await client.DeleteIntentAsync(intentTest.Id, _appId, "1.0");

            var newId = await client.AddIntentAsync("IntentTest", _appId, "1.0");
            Assert.IsNotNull(newId);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnIntentNewIntentTestWhenAlreadyExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddIntentAsync("IntentTest", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "An intent classifier with the same name already exists");
        }

        [TestMethod]
        public async Task ShouldRenameIntentTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            var intentChanged = await client.GetIntentByNameAsync("IntentTestChanged", _appId, "1.0");

            if (intent == null)
            {
                await client.AddIntentAsync("IntentTest", _appId, "1.0");
                intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            }

            if (intentChanged != null)
                await client.DeleteIntentAsync(intentChanged.Id, _appId, "1.0");

            await client.RenameIntentAsync(intent.Id, "IntentTestChanged", _appId, "1.0");

            intent = await client.GetIntentByIdAsync(intent.Id, _appId, "1.0");
            Assert.AreEqual("IntentTestChanged", intent.Name);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenExistsIntentWithSameName()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            var intentChanged = await client.GetIntentByNameAsync("IntentTestChanged", _appId, "1.0");
            string intentChangedId = null;

            if (intent == null)
            {
                await client.AddIntentAsync("IntentTest", _appId, "1.0");
                intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            }
            if (intentChanged == null)
                intentChangedId = await client.AddIntentAsync("IntentTestChanged", _appId, "1.0");

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameIntentAsync(intent.Id, "IntentTestChanged", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The application already contains an intent classifier with the same name");
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.RenameIntentAsync("51593248-363e-4a08-b946-2061964dc690", "IntentTest", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The specified model does not exist in the specified application.");
        }

        [TestMethod]
        public async Task ShouldDeleteIntentTest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            if (await client.GetIntentByNameAsync("IntentTest", _appId, "1.0") == null)
                await client.AddIntentAsync("IntentTest", _appId, "1.0");

            var intent = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            await client.DeleteIntentAsync(intent.Id, _appId, "1.0");
            intent = await client.GetIntentByIdAsync(intent.Id, _appId, "1.0");

            Assert.IsNull(intent);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnDeleteIntentTestWhenNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.DeleteIntentAsync("51593248-363e-4a08-b946-2061964dc690", _appId, "1.0"));

            Assert.AreEqual(ex.Message, "The specified model does not exist in the application");
        }
    }
}
