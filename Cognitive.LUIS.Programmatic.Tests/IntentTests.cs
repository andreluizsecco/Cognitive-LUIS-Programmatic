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
                var intents = await client.Intents.GetAllAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<Intent>>(intents);
            }
        }

        [Fact]
        public async Task ShouldGetExistIntentById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intents = await client.Intents.GetAllAsync(appId, appVersion);

                var firstIntent = intents.FirstOrDefault();

                var intent = await client.Intents.GetByIdAsync(firstIntent.Id, appId, appVersion);
                Assert.Equal(firstIntent.Name, intent.Name);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsIntentId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.Intents.GetByIdAsync(InvalidId, appId, appVersion);
                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldGetIntentByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

                var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                Assert.NotNull(intent);
            }
        }

        [Fact]
        public async Task ShouldGetNullWhenNotExistsIntentName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.Intents.DeleteAsync(intentTest.Id, appId, appVersion);

                var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldAddNewIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.Intents.DeleteAsync(intentTest.Id, appId, appVersion);

                var newId = await client.Intents.AddAsync(IntentName, appId, appVersion);
                Assert.NotNull(newId);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnIntentNewIntentTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                if (intentTest == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Intents.AddAsync(IntentName, appId, appVersion));

                Assert.Equal("BadArgument - The models: { IntentTest } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldRenameIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                var intentChanged = await client.Intents.GetByNameAsync(IntentNameChanged, appId, appVersion);

                if (intent == null)
                {
                    await client.Intents.AddAsync(IntentName, appId, appVersion);
                    intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                }

                if (intentChanged != null)
                    await client.Intents.DeleteAsync(intentChanged.Id, appId, appVersion);

                await client.Intents.RenameAsync(intent.Id, IntentNameChanged, appId, appVersion);

                intent = await client.Intents.GetByIdAsync(intent.Id, appId, appVersion);
                Assert.Equal(IntentNameChanged, intent.Name);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenExistsIntentWithSameName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                var intentChanged = await client.Intents.GetByNameAsync(IntentNameChanged, appId, appVersion);
                string intentChangedId = null;

                if (intent == null)
                {
                    await client.Intents.AddAsync(IntentName, appId, appVersion);
                    intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                }
                if (intentChanged == null)
                    intentChangedId = await client.Intents.AddAsync(IntentNameChanged, appId, appVersion);

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Intents.RenameAsync(intent.Id, IntentNameChanged, appId, appVersion));

                Assert.Equal("BadArgument - The models: { IntentTestChanged } already exist in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Intents.RenameAsync(InvalidId, IntentName, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 51593248-363e-4a08-b946-2061964dc690 in the specified application version.", ex.Message);
            }
        }

        [Fact]
        public async Task ShouldDeleteIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

                var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                await client.Intents.DeleteAsync(intent.Id, appId, appVersion);
                intent = await client.Intents.GetByIdAsync(intent.Id, appId, appVersion);

                Assert.Null(intent);
            }
        }

        [Fact]
        public async Task ShouldDeleteIntentAndUtterancesTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

                // Add example for the intent 
                var exampleAdded = await client.Examples.AddAsync(appId, appVersion, new Example()
                {
                    IntentName = IntentName,
                    Text = "This is sample utterance"
                });

                if (!string.IsNullOrEmpty(exampleAdded?.UtteranceText))
                {

                    var intent = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                    await client.Intents.DeleteAsync(intent.Id, appId, appVersion, true);

                    // TODO : once the get exampleById available, get the exmaple and assert for null
                    intent = await client.Intents.GetByIdAsync(intent.Id, appId, appVersion);

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
                    client.Intents.DeleteAsync(InvalidId, appId, appVersion));

                Assert.Equal("BadArgument - Cannot find model 00000000-0000-0000-0000-000000000000 in the specified application version.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
