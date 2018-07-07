using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class PublishTests : BaseTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context) =>
            Initialize();

        [ClassCleanup]
        public static void ClassCleanup() =>
            Cleanup();

        [TestMethod]
        public async Task ShouldSendPublishRequest()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                await client.AddIntentAsync("IntentTest", appId, appVersion);

                await client.AddExampleAsync(appId, appVersion, new Example
                {
                    Text = "Hello World!",
                    IntentName = "IntentTest"
                });

                var trainingDetails = await client.TrainAndGetFinalStatusAsync(appId, appVersion);
                if (trainingDetails.Status.Equals("Success"))
                {
                    var publish = await client.PublishAsync(appId, appVersion, false, "westus");
                    Assert.IsNotNull(publish);
                }
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnPublishModelWhenAppNotExists()
        {
            using (var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.PublishAsync(InvalidId, appVersion, false, "westus"));

                Assert.AreEqual("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }
    }
}
