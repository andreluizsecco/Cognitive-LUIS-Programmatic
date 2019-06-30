using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class ExampleTests : BaseTest
    {
        public const string IntentName = "IntentTest";

        public ExampleTests() =>
            Initialize();

        [Fact]
        public async Task ShouldGetLabeledExamplesList()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var examples = await client.Examples.GetAllAsync(appId, appVersion);
                Assert.IsAssignableFrom<IEnumerable<ReviewExample>>(examples);
            }
        }

        [Fact]
        public async Task ShouldAddExample()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                string intentTestId = null;
                var intentTest = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    intentTestId = intentTest.Id;
                else
                    intentTestId = await client.Intents.AddAsync(IntentName, appId, appVersion);

                var example = new Example
                {
                    Text = "Hello World!",
                    IntentName = IntentName
                };

                var utterance = await client.Examples.AddAsync(appId, appVersion, example);

                Assert.NotNull(utterance);
            }
        }
        [Fact]
        public async Task ShouldAddLabelledExample()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Add simple entity name if not already exists
                if (await client.Entities.GetSimpleEntityByNameAsync("name", appId, appVersion) == null)
                    await client.Entities.AddSimpleEntityAsync("name", appId, appVersion);

                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

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

                var utterance = await client.Examples.AddAsync(appId, appVersion, labeledExample);

                Assert.NotNull(utterance);
            }
        }

        [Fact]
        public async Task ShoulAddBatchExample()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

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

                var addExamples = await client.Examples.AddBatchAsync(appId, appVersion, examples.ToArray());

                Assert.Equal(2, addExamples.Length);
            }
        }


        [Fact]
        public async Task ShoulAddBatchLbeledExample()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                // Add simple entity name if not already exists
                if (await client.Entities.GetSimpleEntityByNameAsync("name", appId, appVersion) == null)
                    await client.Entities.AddSimpleEntityAsync("name", appId, appVersion);

                // Add simple intent name if not already exists
                if (await client.Intents.GetByNameAsync(IntentName, appId, appVersion) == null)
                    await client.Intents.AddAsync(IntentName, appId, appVersion);

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

                var addExamples = await client.Examples.AddBatchAsync(appId, appVersion, examples.ToArray());

                Assert.False(addExamples[0].HasError);
                Assert.False(addExamples[1].HasError);
            }
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddExampleWhenIntentTestNotExists()
        {
            using(var client = new LuisProgClient(SubscriptionKey, Region))
            {
                var intentTest = await client.Intents.GetByNameAsync(IntentName, appId, appVersion);
                if (intentTest != null)
                    await client.Intents.DeleteAsync(intentTest.Id, appId, appVersion);

                var example = new Example
                {
                    Text = "Hello World!",
                    IntentName = IntentName
                };

                var ex = await Assert.ThrowsAsync<Exception>(() =>
                    client.Examples.AddAsync(appId, appVersion, example));

                Assert.Equal("BadArgument - The intent classifier IntentTest does not exist in the application version.", ex.Message);
            }
        }

        public override void Dispose() =>
            Cleanup();
    }
}
