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
        private readonly string _subscriptionKey;
        private readonly Regions _region;

        private IAppService _apps;
        public IAppService Apps
        {
            get
            {
                if(_apps == null)
                    _apps = new AppService(_subscriptionKey, _region);
                return _apps;
            }
        }

        private IEntityService _entities;
        public IEntityService Entities
        {
            get
            {
                if (_entities == null)
                    _entities = new EntityService(_subscriptionKey, _region);
                return _entities;
            }
        }

        private IExampleService _examples;
        public IExampleService Examples
        {
            get
            {
                if (_examples == null)
                    _examples = new ExampleService(_subscriptionKey, _region);
                return _examples;
            }
        }

        private IIntentService _intents;
        public IIntentService Intents
        {
            get
            {
                if (_intents == null)
                    _intents = new IntentService(_subscriptionKey, _region);
                return _intents;
            }
        }

        private IPublishService _publishing;
        public IPublishService Publishing
        {
            get
            {
                if (_publishing == null)
                    _publishing = new PublishService(_subscriptionKey, _region);
                return _publishing;
            }
        }

        private IVersionService _versions;
        public IVersionService Versions
        {
            get
            {
                if (_versions == null)
                    _versions = new VersionService(_subscriptionKey, _region);
                return _versions;
            }
        }

        private ITrainingService _training;
        public ITrainingService Training
        {
            get
            {
                if (_training == null)
                    _training = new TrainingService(_subscriptionKey, _region);
                return _training;
            }
        }

        /// <param name="subscriptionKey">LUIS Authoring Key</param>
        /// <param name="region">Regions currently available in West US, West Europe and Australia East".</param>
        public LuisProgClient(string subscriptionKey, Regions region)
        {
            if (string.IsNullOrWhiteSpace(subscriptionKey))
                throw new ArgumentNullException(nameof(subscriptionKey));

            _subscriptionKey = subscriptionKey;
            _region = region;
        }

        public void Dispose()
        {
            _apps?.Dispose();
            _entities?.Dispose();
            _examples?.Dispose();
            _intents?.Dispose();
            _publishing?.Dispose();
            _versions?.Dispose();
            _training?.Dispose();
        }
    }
}
