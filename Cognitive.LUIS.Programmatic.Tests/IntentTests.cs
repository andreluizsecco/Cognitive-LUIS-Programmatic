using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class IntentTests : BaseTest
    {
        public const string IntentName = "IntentTest";
        public const string IntentNameChanged = "IntentTestChanged";

        public IntentTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetIntentList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intents = await client.GetAllIntentsAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<Intent>>(intents);
            }
        }

        [Fact]
        public async Task ShouldGetExistIntentById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intents = await client.GetAllIntentsAsync(appId, appVersion);

                var firstIntent = intents.FirstOrDefault();

                var intent = await client.GetIntentByIdAsync(firstIntent.Id, appId, appVersion);
                Assert.Equal(firstIntent.Name, intent.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsIntentId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.GetIntentByIdAsync(InvalidId, appId, appVersion);
                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldGetIntentByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                Assert.NotNull(intent);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsIntentName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.DeleteIntentAsync(intentTest.Id, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldAddNewIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.DeleteIntentAsync(intentTest.Id, appId, appVersion);

                var newId = await client.AddIntentAsync(IntentName, appId, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnIntentNewIntentTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.AddIntentAsync(IntentName, appId, appVersion));

                Assert.Equal("BadArgument - The models: { IntentTest } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                var intentChanged = await client.GetIntentByNameAsync(IntentNameChanged, appId, appVersion);

                if (intent == null)
                {
                    await client.AddIntentAsync(IntentName, appId, appVersion);
                    intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                }

                if (intentChanged != null)
                    await client.DeleteIntentAsync(intentChanged.Id, appId, appVersion);

                await client.RenameIntentAsync(intent.Id, IntentNameChanged, appId, appVersion);

                intent = await client.GetIntentByIdAsync(intent.Id, appId, appVersion);
                Assert.Equal(IntentNameChanged, intent.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenExistsIntentWithSameName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                var intentChanged = await client.GetIntentByNameAsync(IntentNameChanged, appId, appVersion);
                string intentChangedId = null;

                if (intent == null)
                {
                    await client.AddIntentAsync(IntentName, appId, appVersion);
                    intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                }
                if (intentChanged == null)
                    intentChangedId = await client.AddIntentAsync(IntentNameChanged, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.RenameIntentAsync(intent.Id, IntentNameChanged, appId, appVersion));

                Assert.Equal("BadArgument - The models: { IntentTestChanged } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.RenameIntentAsync(InvalidId, IntentName, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                await client.DeleteIntentAsync(intent.Id, appId, appVersion);
                intent = await client.GetIntentByIdAsync(intent.Id, appId, appVersion);

                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldDeleteIntentAndUtterancesTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                // Add example for the intent 
                var exampleAdded = await client.AddExampleAsync(appId, appVersion, new Example()
                {
                    IntentName = IntentName,
                    Text = "This is sample utterance"
                });

                if (!string.IsNullOrEmpty(exampleAdded?.UtteranceText))
                {

                    var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                    await client.DeleteIntentAsync(intent.Id, appId, appVersion, true);

                    // TODO : once the get exampleById available, get the exmaple and assert for null
                    intent = await client.GetIntentByIdAsync(intent.Id, appId, appVersion);

                    Assert.Null(intent);
                }
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteIntentTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.DeleteIntentAsync(InvalidId, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 00000000-0000-0000-0000-000000000000 in the specified application version.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
