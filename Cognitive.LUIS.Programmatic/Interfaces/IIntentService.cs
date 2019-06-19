using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Intents
{
    public interface IIntentService : IDisposable
    {
        /// <summary>
        /// Gets information about the intent models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of app intents</returns>
        Task<IReadOnlyCollection<Intent>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        Task<Intent> GetByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        Task<Intent> GetByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Creates a new app intent and returns the id
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created intent</returns>
        Task<string> AddAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Change the name of app intent
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="name">new intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task RenameAsync(string id, string name, string appId, string appVersionId);

        /// <summary>
        /// Deletes an intent classifier from the application. All the utterances will be moved under None intent if deleteUtterance is false(default behavior).
        /// To delete all the utterances of the intent pass the deleteUtterance parameter value as true.
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="deleteUtterances">delete utterances flag. Optional paramater with default value 'false'.</param>
        /// <returns></returns>
        Task DeleteAsync(string id, string appId, string appVersionId, bool deleteUtterances = false);
    }
}