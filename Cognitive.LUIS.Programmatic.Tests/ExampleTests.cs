using Cognitive.LUIS.Programmatic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private const string SUBSCRIPTION_KEY = "{YourSubscriptionKey}";
        private const Location LOCATION = Location.WestUS;
        private readonly string _appId;

        public ExampleTests()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                _appId = app.Id;
            else
                _appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, "1.0").Result;
        }

        [TestMethod]
        public async Task ShouldGetLabeledExamplesList()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var examples = await client.GetAllLabeledExamplesAsync(_appId, "1.0");
            Assert.IsInstanceOfType(examples, typeof(IEnumerable<ReviewExample>));
        }

        [TestMethod]
        public async Task ShouldAddExample()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            string intentTestId = null;
            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                intentTestId = intentTest.Id;
            else
                intentTestId = await client.AddIntentAsync("IntentTest", _appId, "1.0");

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            };

            var utterance = await client.AddExampleAsync(_appId, "1.0", example);

            Assert.IsNotNull(utterance);
        }
        [TestMethod]
        public async Task ShouldAddLabelledExample()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);

            // Add simple entity name if not already exists
            if (await client.GetEntityByNameAsync("name", _appId, "1.0") == null)
                await client.AddEntityAsync("name", _appId, "1.0");

            if (await client.GetIntentByNameAsync("IntentTest", _appId, "1.0") == null)
                await client.AddIntentAsync("IntentTest", _appId, "1.0");

            var labeledExample = new Example()
            {
                Text = "Who is Test User!",
                IntentName = "IntentTest", 
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

            var utterance = await client.AddExampleAsync(_appId, "1.0", labeledExample);

            Assert.IsNotNull(utterance);
        }

        [TestMethod]
        public async Task ShoulAddBatchExample()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);

            if (await client.GetIntentByNameAsync("IntentTest", _appId, "1.0") == null)
                await client.AddIntentAsync("IntentTest", _appId, "1.0");

            List<Example> examples = new List<Example>();
            examples.Add(new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            });

            examples.Add(new Example
            {
                Text = "This is a test Utterance",
                IntentName = "IntentTest"
            });

            var addExamples = await client.AddBatchExampleAsync(_appId, "1.0", examples.ToArray());

            Assert.AreEqual<int>(2, addExamples.Length);
        }


        [TestMethod]
        public async Task ShoulAddBatchLbeledExample()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            // Add simple entity name if not already exists
            if (await client.GetEntityByNameAsync("name", _appId, "1.0") == null)
                await client.AddEntityAsync("name", _appId, "1.0");

            // Add simple intent name if not already exists
            if (await client.GetIntentByNameAsync("IntentTest", _appId, "1.0") == null)
                await client.AddIntentAsync("IntentTest", _appId, "1.0");

            List<Example> examples = new List<Example>();
            examples.Add(new Example()
            {
                Text = "Who is Bill?",
                IntentName = "IntentTest",
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
                IntentName = "IntentTest",
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

            var addExamples = await client.AddBatchExampleAsync(_appId, "1.0", examples.ToArray());

            Assert.AreEqual<bool>(false, addExamples[0].HasError);
            Assert.AreEqual<bool>(false, addExamples[1].HasError);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionOnAddExampleWhenIntentTestNotExists()
        {
            var client = new LuisProgClient(SUBSCRIPTION_KEY, LOCATION);
            var intentTest = await client.GetIntentByNameAsync("IntentTest", _appId, "1.0");
            if (intentTest != null)
                await client.DeleteIntentAsync(intentTest.Id, _appId, "1.0");

            var example = new Example
            {
                Text = "Hello World!",
                IntentName = "IntentTest"
            };

            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                client.AddExampleAsync(_appId, "1.0", example));

            Assert.AreEqual(ex.Message, "The intent classifier IntentTest does not exist in the selected application");
        }
    }
}
