using System;
using Cognitive.LUIS.Programmatic.Apps;
using Cognitive.LUIS.Programmatic.Entities;
using Cognitive.LUIS.Programmatic.Examples;
using Cognitive.LUIS.Programmatic.Intents;
using Cognitive.LUIS.Programmatic.Training;
using Cognitive.LUIS.Programmatic.Versions;

namespace Cognitive.LUIS.Programmatic
{
    public class LuisProgClient : IDisposable
    {
        public IAppService Apps { get; }
        public IEntityService Entities { get; }
        public IExampleService Examples { get; }
        public IIntentService Intents { get; }
        public IPublishService Publishing { get; }
        public IVersionService Versions { get; }
        public ITrainingService Training { get; }

        /// <param name="subscriptionKey">LUIS Authoring Key</param>
        /// <param name="region">Regions currently available in West US, West Europe and Australia East".</param>
        public LuisProgClient(string subscriptionKey, Regions region)
        {
            Apps = new AppService(subscriptionKey, region);
            Entities = new EntityService(subscriptionKey, region);
            Examples = new ExampleService(subscriptionKey, region);
            Intents = new IntentService(subscriptionKey, region);
            Publishing = new PublishService(subscriptionKey, region);
            Versions = new VersionService(subscriptionKey, region);
            Training = new TrainingService(subscriptionKey, region);
        }

        public void Dispose()
        {
            Apps?.Dispose();
            Entities?.Dispose();
            Examples?.Dispose();
            Intents?.Dispose();
            Publishing?.Dispose();
            Versions?.Dispose();
            Training?.Dispose();
        }
    }
}
