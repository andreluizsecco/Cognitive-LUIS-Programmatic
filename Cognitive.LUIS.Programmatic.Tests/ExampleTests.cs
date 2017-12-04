using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private readonly string _appId;

        public ExampleTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                _appId = app.Id;
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldAddExample()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            string intentTestId = null;
            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                intentTestId = intentTest.Id;
            else
                intentTestId = await client.AddIntentAsync("IntentTest", _appId, "1.0");

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            };

            var utterance = await client.AddExampleAsync(_appId, "1.0", example);

            Assert.IsNotNull(utterance);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnAddExampleWhenIntentTestNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY);
            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                await client.DeleteIntentAsync(intentTest.Id, _appId, "1.0");

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            };

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddExampleAsync(_appId, "1.0", example));

            Assert.AreEqual(ex.Message, "The intent classifier IntentTest does not exist in the selected application");
        }
    }
}
