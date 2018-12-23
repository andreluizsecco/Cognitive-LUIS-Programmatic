using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class TrainingTests : BaseTest
    {
        public TrainingTests() =>
            Initialize();

        [Fact]
        public async Task ShouldSendTrainingRequest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var trainingDetails = await client.TrainAsync(appId, appVersion);
                Assert.NotNull(trainingDetails);
            }
        }

        [Fact]
        public async Task ShouldGetTrainingStatusList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var training = await client.GetTrainingStatusListAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<Training>>(training);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnTrainModelWhenAppNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.TrainAsync(InvalidId, appVersion));

                Assert.Equal("BadArgument - Cannot find an application with the ID 51593248-363e-4a08-b946-2061964dc690.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldSendTrainAndGetFinalStatus()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var trainingDetails = await client.TrainAndGetFinalStatusAsync(appId, appVersion);

                Assert.NotNull(trainingDetails);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
