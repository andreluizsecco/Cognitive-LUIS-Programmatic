using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class IntentTests : BaseTest
    {
        public const string IntentName = "IntentTest";
        public const string IntentNameChanged = "IntentTestChanged";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) =>
            Initialize();

        [ClassCleanup]
        public static void ClassCleanup() =>
            Cleanup();

        [TestMethod]
        public async Task ShouldGetIntentList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intents = await client.GetAllIntentsAsync(appId, appVersion);
                Assert.IsInstanceOfType(intents, typeof(IEnumerable<Intent>));
            }
        }

        [TestMethod]
        public async Task ShouldGetExistIntentById()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intents = await client.GetAllIntentsAsync(appId, appVersion);

                var firstIntent = intents.FirstOrDefault();

                var intent = await client.GetIntentByIdAsync(firstIntent.Id, appId, appVersion);
                Assert.AreEqual(firstIntent.Name, intent.Name);
            }
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsIntentId()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intent = await client.GetIntentByIdAsync(InvalidId, appId, appVersion);
                Assert.IsNull(intent);
            }
        }

        [TestMethod]
        public async Task ShouldGetIntentByName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                Assert.IsNotNull(intent);
            }
        }

        [TestMethod]
        public async Task ShouldGetNullWhenNotExistsIntentName()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.DeleteIntentAsync(intentTest.Id, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                Assert.IsNull(intent);
            }
        }

        [TestMethod]
        public async Task ShouldAddNewIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.DeleteIntentAsync(intentTest.Id, appId, appVersion);

                var newId = await client.AddIntentAsync(IntentName, appId, appVersion);
                Assert.IsNotNull(newId);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnIntentNewIntentTestWhenAlreadyExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                if (intentTest == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.AddIntentAsync(IntentName, appId, appVersion));

                Assert.AreEqual("BadArgument - The models: { IntentTest } already exist in the specified application version.", ex.Message);
            }
        }

        [TestMethod]
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
                Assert.AreEqual(IntentNameChanged, intent.Name);
            }
        }

        [TestMethod]
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

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.RenameIntentAsync(intent.Id, IntentNameChanged, appId, appVersion));

                Assert.AreEqual("BadArgument - The models: { IntentTestChanged } already exist in the specified application version.", ex.Message);
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnRenameIntentTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.RenameIntentAsync(InvalidId, IntentName, appId, appVersion));

                Assert.AreEqual("BadArgument - Cannot find the input model in the specified application version.", ex.Message);
            }
        }

        [TestMethod]
        public async Task ShouldDeleteIntentTest()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                    await client.AddIntentAsync(IntentName, appId, appVersion);

                var intent = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
                await client.DeleteIntentAsync(intent.Id, appId, appVersion);
                intent = await client.GetIntentByIdAsync(intent.Id, appId, appVersion);

                Assert.IsNull(intent);
            }
        }

        [TestMethod]
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

                    Assert.IsNull(intent);
                }
            }
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnDeleteIntentTestWhenNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                    client.DeleteIntentAsync(InvalidId, appId, appVersion));

                Assert.AreEqual("BadArgument - Cannot find the input model in the specified application version.", ex.Message);
            }
        }
    }
}
