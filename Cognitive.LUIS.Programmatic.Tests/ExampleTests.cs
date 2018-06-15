using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class ExampleTests : BaseTest
    {
        public const string IntentName = "IntentTest";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) =>
            Initialize();

        [ClassCleanup]
        public static void ClassCleanup() =>
            Cleanup();

        [TestMethod]
        public async Task ShouldGetLabeledExamplesList()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var examples = await client.GetAllLabeledExamplesAsync(appId, appVersion);
            Assert.IsInstanceOfType(examples, typeof(IEnumerable<ReviewExample>));
        }

        [TestMethod]
        public async Task ShouldAddExample()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            string intentTestId = null;
            var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
            if (intentTest != null)
                intentTestId = intentTest.Id;
            else
                intentTestId = await client.AddIntentAsync(IntentName, appId, appVersion);

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = IntentName
            };

            var utterance = await client.AddExampleAsync(appId, appVersion, example);

            Assert.IsNotNull(utterance);
        }
        [TestMethod]
        public async Task ShouldAddLabelledExample()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);

            // Add simple entity name if not already exists
            if (await client.GetEntityByNameAsync("name", appId, appVersion) == null)
                await client.AddEntityAsync("name", appId, appVersion);

            if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                await client.AddIntentAsync(IntentName, appId, appVersion);

            var labeledExample = new Example()
            {
                Text = "Who is Test User!",
                IntentName = IntentName, 
                EntityLabels = new List<EntityLabel>
                {
                    new EntityLabel
                    {
                        EntityName = "name", 
                        StartCharIndex = 7,
                        EndCharIndex = 15
                    }
                }
            };

            var utterance = await client.AddExampleAsync(appId, appVersion, labeledExample);

            Assert.IsNotNull(utterance);
        }

        [TestMethod]
        public async Task ShoulAddBatchExample()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);

            if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                await client.AddIntentAsync(IntentName, appId, appVersion);

            List<Example> examples = new List<Example>();
            examples.Add(new Example
            {
                Text = "Hello World!",
                IntentName = IntentName
            });

            examples.Add(new Example
            {
                Text = "This is a test Utterance",
                IntentName = IntentName
            });

            var addExamples = await client.AddBatchExampleAsync(appId, appVersion, examples.ToArray());

            Assert.AreEqual<int>(2, addExamples.Length);
        }


        [TestMethod]
        public async Task ShoulAddBatchLbeledExample()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            // Add simple entity name if not already exists
            if (await client.GetEntityByNameAsync("name", appId, appVersion) == null)
                await client.AddEntityAsync("name", appId, appVersion);

            // Add simple intent name if not already exists
            if (await client.GetIntentByNameAsync(IntentName, appId, appVersion) == null)
                await client.AddIntentAsync(IntentName, appId, appVersion);

            List<Example> examples = new List<Example>();
            examples.Add(new Example()
            {
                Text = "Who is Bill?",
                IntentName = IntentName,
                EntityLabels = new List<EntityLabel>
                {
                    new EntityLabel
                    {
                        EntityName = "name",
                        StartCharIndex = 7,
                        EndCharIndex = 10
                    }
                }
            });

            examples.Add(new Example()
            {
                Text = "Who is Christopher?",
                IntentName = IntentName,
                EntityLabels = new List<EntityLabel>
                {
                    new EntityLabel
                    {
                        EntityName = "name",
                        StartCharIndex = 7,
                        EndCharIndex = 17
                    }
                }
            });

            var addExamples = await client.AddBatchExampleAsync(appId, appVersion, examples.ToArray());

            Assert.AreEqual<bool>(false, addExamples[0].HasError);
            Assert.AreEqual<bool>(false, addExamples[1].HasError);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnAddExampleWhenIntentTestNotExists()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var intentTest = await client.GetIntentByNameAsync(IntentName, appId, appVersion);
            if (intentTest != null)
                await client.DeleteIntentAsync(intentTest.Id, appId, appVersion);

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = IntentName
            };

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddExampleAsync(appId, appVersion, example));

            Assert.AreEqual(ex.Message, "BadArgument - The intent classifier IntentTest does not exist in the application version.");
        }
    }
}
