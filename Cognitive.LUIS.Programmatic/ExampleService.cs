using System;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IExampleService
    {
        /// <summary>
        /// Adds a labeled example to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="model">object containing the labeled example</param>
        /// <returns>A object of utterance created</returns>
        public async Task<Utterance> AddExampleAsync(string appId, string appVersionId, Example model)
        {
            var response = await Post($"apps/{appId}/versions/{appVersionId}/example", model);
            return JsonConvert.DeserializeObject<Utterance>(response);
        }

        /// <summary>
        /// Adds batch of labeled examples to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="models">array of objects containing the labeled examples</param>
        /// <returns></returns>
        public async Task<BatchExample[]> AddBatchExampleAsync(string appId, string appVersionId, Example[] models)
        {
            if (models.Length < 100)
            {
                var response = await Post($"apps/{appId}/versions/{appVersionId}/examples", models);
                return JsonConvert.DeserializeObject<BatchExample[]>(response);
            }
            else
                throw new Exception("Batch limit exceeded. The maximum batch size is 100 items.");
        }

        /// <summary>
        /// Deletes a example from the application.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="exampleId">labeled example id</param>
        /// <returns></returns>
        public async Task DeleteExampleAsync(string appId, string appVersionId, string exampleId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/examples/{exampleId}");
        }
    }
}