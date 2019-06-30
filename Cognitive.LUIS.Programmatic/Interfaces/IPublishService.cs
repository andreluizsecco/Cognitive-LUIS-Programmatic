using System;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IPublishService : IDisposable
    {
        /// <summary>
        /// Publishes a specific version of the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="isStaging">The flag "isStaging" should be set to true in case you want to publish to the STAGING slot, otherwise the application version will be published to the PRODUCTION slot</param>
        /// <param name="directVersionPublish">In case you do not want to publish to either the PRODUCTION or STAGING slots, you can set the flag "directVersionPublish" to true and query the endpoint [directly using the versionId] (https://westus.dev.cognitive.microsoft.com/docs/services/luis-endpoint-api-v3-0-preview/operations/5cb0a9459a1fe8fa44c28dd8).</param>
        /// <returns>A object of publish details</returns>
        Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging = false, bool directVersionPublish = false);
    }
}