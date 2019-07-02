using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic.Training
{
    public class TrainingService : ServiceClient, ITrainingService
    {
        public TrainingService(string subscriptionKey, Regions region, RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(subscriptionKey, region, retryPolicyConfiguration) { }

        /// <summary>
        /// Sends a training request for a version of a specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A object of training request details</returns>
        public async Task<TrainingDetails> TrainAsync(string appId, string appVersionId)
        {
            var response = await Post($"apps/{appId}/versions/{appVersionId}/train");
            return JsonConvert.DeserializeObject<TrainingDetails>(response);
        }

        /// <summary>
        /// Gets the training status of all models (intents and entities) for the specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A list of trainings status</returns>
        public async Task<IReadOnlyCollection<Models.Training>> GetStatusListAsync(string appId, string appVersionId)
        {
            IReadOnlyCollection<Models.Training> trainings = Array.Empty<Models.Training>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/train");
            if (response != null)
                trainings = JsonConvert.DeserializeObject<IReadOnlyCollection<Models.Training>>(response);
            return trainings;
        }
        
        /// <summary>
        /// Requests train and wait till the training completes, returns the final status.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="timeout">maximum wait time to return the final status (in seconds)</param>
        /// <returns>Training details object</returns>
        public async Task<TrainingDetails> TrainAndGetFinalStatusAsync(string appId, string appVersionId, int timeout = 60)
        {
            var response = await Post($"apps/{appId}/versions/{appVersionId}/train");
            IEnumerable<Models.Training> trainingStatusList = null;
            var maximumWaitTime = DateTime.Now.AddSeconds(timeout);
            
            bool wait = true;
            IEnumerable<TrainingStatus> statusList;
            do
            {
                if (DateTime.Now > maximumWaitTime)
                    throw new Exception("Request timeout: LUIS application training is taking too long.");
                    
                response = await Get($"apps/{appId}/versions/{appVersionId}/train");
                if (response != null)
                    trainingStatusList = JsonConvert.DeserializeObject<IReadOnlyCollection<Models.Training>>(response);

                statusList = trainingStatusList.Select(x => (TrainingStatus)x.Details.StatusId);
                wait = statusList.Any(x => (x == TrainingStatus.InProgress || x == TrainingStatus.Queued) && x != TrainingStatus.Fail);
                
                if (wait)
                    Thread.Sleep(2000);
            }
            while (wait);

            return trainingStatusList.First().Details;
        }
    }
}