using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Examples
{
    public interface IExampleService : IDisposable
    {
        /// <summary>
        /// Gets examples to be reviewed.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A list of examples to be reviewed</returns>
        Task<IReadOnlyCollection<ReviewExample>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Adds a labeled example to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="model">object containing the labeled example</param>
        /// <returns>A object of utterance created</returns>
        Task<Utterance> AddAsync(string appId, string appVersionId, Example model);

        /// <summary>
        /// Adds batch of labeled examples to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="models">array of objects containing the labeled examples</param>
        /// <returns></returns>
        Task<BatchExample[]> AddBatchAsync(string appId, string appVersionId, Example[] model);

        /// <summary>
        /// Deletes a example from the application.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="exampleId">labeled example id</param>
        /// <returns></returns>
        Task DeleteAsync(string appId, string appVersionId, string exampleId);
    }
}