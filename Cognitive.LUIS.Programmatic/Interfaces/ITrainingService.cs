using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Training
{
    public interface ITrainingService : IDisposable
    {
        /// <summary>
        /// Sends a training request for a version of a specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A object of training request details</returns>
        Task<TrainingDetails> TrainAsync(string appId, string appVersionId);

        /// <summary>
        /// Gets the training status of all models (intents and entities) for the specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A list of trainings status</returns>
        Task<IReadOnlyCollection<Models.Training>> GetStatusListAsync(string appId, string appVersionId);

        /// <summary>
        /// Requests train and wait till the training completes, returns the final status.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="timeout">maximum wait time to return the final status (in seconds)</param>
        /// <returns>Training details object</returns>
        Task<TrainingDetails> TrainAndGetFinalStatusAsync(string appId, string appVersionId, int timeoutt = 60);
    }
}