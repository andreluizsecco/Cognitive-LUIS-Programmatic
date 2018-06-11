using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IIntentService
    {
        /// <summary>
        /// Gets information about the intent models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of app intents</returns>
        public async Task<IReadOnlyCollection<Intent>> GetAllIntentsAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<Intent> intents = Array.Empty<Intent>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/intents");
            if (response != null)
                intents = JsonConvert.DeserializeObject<IReadOnlyCollection<Intent>>(response);
            return intents;
        }

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        public async Task<Intent> GetIntentByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/intents/{id}");
            return JsonConvert.DeserializeObject<Intent>(response);
        }

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        public async Task<Intent> GetIntentByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllIntentsAsync(appId, appVersionId);
            if (apps != null)
                return apps.FirstOrDefault(intent => intent.Name.Equals(name));
            else
                return null;
        }

        /// <summary>
        /// Creates a new app intent and returns the id
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created intent</returns>
        public async Task<string> AddIntentAsync(string name, string appId, string appVersionId)
        {
            var intent = new
            {
                name = name
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/intents", intent);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Change the name of app intent
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="name">new intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task RenameIntentAsync(string id, string name, string appId, string appVersionId)
        {
            var intent = new
            {
                name = name
            };
            await Put($"apps/{appId}/versions/{appVersionId}/intents/{id}", intent);
        }

        /// <summary>
        /// Deletes an intent classifier from the application. All the utterances will be moved under None intent if deleteUtterance is false(default behavior).
        /// To delete all the utterances of the intent pass the deleteUtterance parameter value as true.
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="deleteUtterances">delete utterances flag. Optional paramater with default value 'false'.</param>
        /// <returns></returns>
        public async Task DeleteIntentAsync(string id, string appId, string appVersionId, bool deleteUtterances = false) =>
            await Delete($"apps/{appId}/versions/{appVersionId}/intents/{id}?deleteUtterances={deleteUtterances}");
    }
}