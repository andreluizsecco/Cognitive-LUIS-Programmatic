using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class TrainingTests : BaseTest
    {

        [TestMethod]
        public async Task ShouldSendTrainingRequest()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var trainingDetails = await client.TrainAsync(appId, appVersion);

            Assert.IsNotNull(trainingDetails);
        }

        [TestMethod]
        public async Task ShouldGetTrainingStatusList()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var training = await client.GetTrainingStatusListAsync(appId, appVersion);

            Assert.IsInstanceOfType(training, typeof(IEnumerable<Training>));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnTrainModelWhenAppNotExists()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.TrainAsync(InvalidId, appVersion));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }

        [TestMethod]
        public async Task ShouldSendTrainAndGetFinalStatus()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var trainingDetails = await client.TrainAndGetFinalStatusAsync(appId, appVersion);

            Assert.IsNotNull(trainingDetails);
        }

    }
}
