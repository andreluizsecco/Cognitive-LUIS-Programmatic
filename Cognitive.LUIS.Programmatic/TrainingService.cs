using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : ITrainingService
    {
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
        public async Task<IReadOnlyCollection<Training>> GetTrainingStatusListAsync(string appId, string appVersionId)
        {
            IReadOnlyCollection<Training> trainings = Array.Empty<Training>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/train");
            if (response != null)
                trainings = JsonConvert.DeserializeObject<IReadOnlyCollection<Training>>(response);
            return trainings;
        }
        /// <summary>
        /// Requests train and wait till the training complets, returns the final status.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        public async Task<TrainingDetails> TrainAndGetCompletionStatusAsync(string appId, string appVersionId)
        {
            var response = await Post($"apps/{appId}/versions/{appVersionId}/train");

            // Ignore the first response :TODO
            TrainingDetails trainingDetails = JsonConvert.DeserializeObject<TrainingDetails>(response);

            // if training status(2) is up to date return the final training status.
            if (trainingDetails.StatusId.Equals(TrainingStatus.UpToDate))
                return trainingDetails;

            // else pool for status with max timeout 10 minutes
            DateTime maximumWaitTime = DateTime.Now.AddMinutes(10);

            IReadOnlyCollection<Training> trainingStatusList;
            int pendingCount = int.MaxValue;

            while ((pendingCount > 0) && (DateTime.Now < maximumWaitTime))
            {
                // Sleep for 2 seconds
                Thread.Sleep(2000);
                pendingCount = 0;

                var trainStatusResponse = await Get($"apps/{appId}/versions/{appVersionId}/train");
                if (trainStatusResponse != null)
                {
                    trainingStatusList = JsonConvert.DeserializeObject<IReadOnlyCollection<Training>>(trainStatusResponse);
                    foreach (Training trainingStatus in trainingStatusList)
                    {
                        trainingDetails = trainingStatus.Details;
                        switch ((TrainingStatus)trainingDetails.StatusId)
                        {
                            case TrainingStatus.InProgress:
                            case TrainingStatus.Queued:
                                pendingCount++;
                                break;
                            case TrainingStatus.Fail:
                                return trainingDetails;
                        }
                    }
                }
            }

            if (pendingCount > 0)
                throw new Exception("TimeOut Message : LUIS application training is taking too long.");

            return trainingDetails;
        }
    }
}