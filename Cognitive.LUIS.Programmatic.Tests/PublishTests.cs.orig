using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class PublishTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private const Location LOCATION = Location.WestUS;
        private readonly string _appId;

        public PublishTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);

            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
            {
                client.DeleteAppAsync(app.Id).Wait();
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
            }
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldSendPublishRequest()
        {
            IEnumerable<Training> trainingList;
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            await client.AddIntentAsync("IntentTest", _appId, "1.0");

            await client.AddExampleAsync(_appId, "1.0", new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            });
            await client.TrainAsync(_appId, "1.0");

            do
            {
                trainingList = await client.GetTrainingStatusListAsync(_appId, "1.0");
            }
            while (!trainingList.All(x => x.Details.Status.Equals("Success")));

            var publish = await client.PublishAsync(_appId, "1.0", false, "westus");

            Assert.IsNotNull(publish);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnPublishModelWhenAppNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.PublishAsync("51593248-363e-4a08-b946-2061964dc690", "1.0", false, "westus"));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }
    }
}
