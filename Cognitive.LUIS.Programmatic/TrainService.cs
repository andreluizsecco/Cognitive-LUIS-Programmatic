using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : ITrainService
    {
        /// <summary>
        /// Sends a training request for a version of a specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A object of training request details</returns>
        public async Task<TrainingDetails> TrainAsync(string appId, string appVersionId)
        {
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/train");
            return JsonConvert.DeserializeObject<TrainingDetails>(response);
        }

        /// <summary>
        /// Gets the training status of all models (intents and entities) for the specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A list of trainings status</returns>
        public async Task<IEnumerable<Training>> GetTrainingStatusListAsync(string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/train");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Training>>(content);
            else
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
        }
        /// <summary>
        /// Requests train and wait till the training complets, returns the final status.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
         public async Task<TrainingDetails> TrainAndGetCompletionStatusAsync(string appId, string appVersionId)
        {
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/train");
           
            // Ignore the first response :TODO
             TrainingDetails trainingDetails =  JsonConvert.DeserializeObject<TrainingDetails>(response);

            // if training status(2) is up to date return the final training status.
            if (trainingDetails.StatusId.Equals(TrainingStatus.UpToDate))
            {
                return trainingDetails;
            }
          
            // else pool for status with max timeout 15 mins
            DateTime maximumWaitTime = DateTime.Now.AddMinutes(15);

            IEnumerable<Training> trainingStatuses;
            int pendingCount = int.MaxValue;
          
            while ((pendingCount > 0) && (DateTime.Now < maximumWaitTime))
            {
                // Sleep for 2 seconds
                Thread.Sleep(2000);
                pendingCount = 0;
              
                var trainStatusresponse = await Get($"/apps/{appId}/versions/{appVersionId}/train");
                var content = await trainStatusresponse.Content.ReadAsStringAsync();
                if (trainStatusresponse.IsSuccessStatusCode)
                {
                    trainingStatuses = JsonConvert.DeserializeObject<IEnumerable<Training>>(content);
                }
                else
                {
                    var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                    throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
                }

                foreach (Training trainingStatus in trainingStatuses)
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

            if (pendingCount > 0)
            {
                throw new Exception("TimeOut Message : LUIS application training is taking too long.");
            }

            return trainingDetails;
        }
    }
}