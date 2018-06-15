using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public abstract class PublishTests : BaseTest
    {
        public PublishTests()
        {
            Cleanup();
            CreateApp();
        }

        [TestMethod]
        public async Task ShouldSendPublishRequest()
        {
            IEnumerable<Training> trainingList;
            var client = new LuisProgClient(SubscriptionKey, Region);
            await client.AddIntentAsync("IntentTest", appId, appVersion);

            await client.AddExampleAsync(appId, appVersion, new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            });
            await client.TrainAsync(appId, appVersion);

            do
            {
                trainingList = await client.GetTrainingStatusListAsync(appId, appVersion);
            }
            while (!trainingList.All(x => x.Details.Status.Equals("Success")));

            var publish = await client.PublishAsync(appId, appVersion, false, "westus");

            Assert.IsNotNull(publish);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnPublishModelWhenAppNotExists()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.PublishAsync(InvalidId, appVersion, false, "westus"));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }
    }
}
