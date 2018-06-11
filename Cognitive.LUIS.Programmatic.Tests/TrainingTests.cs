using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class TrainingTests
    {
        private const string SUBSCRIPTION_KEY = "72a107783ba845b39e2678d64d3a31a4";
        private const Location LOCATION = Location.WestUS;
        private readonly string _appId;

        public TrainingTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                _appId = app.Id;
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldSendTrainingRequest()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var trainingDetails = await client.TrainAsync(_appId, "1.0");

            Assert.IsNotNull(trainingDetails);
        }

        [TestMethod]
        public async Task ShouldGetTrainingStatusList()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var training = await client.GetTrainingStatusListAsync(_appId, "1.0");

            Assert.IsInstanceOfType(training, typeof(IEnumerable<Training>));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnTrainModelWhenAppNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.TrainAsync("51593248-363e-4a08-b946-2061964dc690", "1.0"));

            Assert.AreEqual(ex.Message, "Cannot find an application with the specified ID");
        }

        [TestMethod]
        public async Task ShouldSendTrainAndGetFinalStatus()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var trainingDetails = await client.TrainAndGetFinalStatusAsync(_appId, "1.0");

            Assert.IsNotNull(trainingDetails);
        }

    }
}
