using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic.Examples
{
    public class ExampleService : ServiceClient, IExampleService
    {
        public ExampleService(string subscriptionKey, Regions region, RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(subscriptionKey, region, retryPolicyConfiguration) { }

        /// <summary>
        /// Gets examples to be reviewed.
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A list of examples to be reviewed</returns>
        public async Task<IReadOnlyCollection<ReviewExample>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<ReviewExample> examples = Array.Empty<ReviewExample>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/examples?skip={skip}&take={take}");
            if (response != null)
                examples = JsonConvert.DeserializeObject<IReadOnlyCollection<ReviewExample>>(response);
            return examples;
        }

        /// <summary>
        /// Adds a labeled example to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="model">object containing the labeled example</param>
        /// <returns>A object of utterance created</returns>
        public async Task<Utterance> AddAsync(string appId, string appVersionId, Example model)
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
        public async Task<BatchExample[]> AddBatchAsync(string appId, string appVersionId, Example[] models)
        {
            if (models.Length <= 100)
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
        public async Task DeleteAsync(string appId, string appVersionId, string exampleId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/examples/{exampleId}");
        }
    }
}