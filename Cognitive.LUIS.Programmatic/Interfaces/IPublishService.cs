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
        /// <param name="isStaging"></param>
        /// <param name="region">The publish location is determined from the creation location. To publish to more than one region, the region properties should be a comma-separated list, "region": "westus, westeurope".</param>
        /// <returns>A object of publish details</returns>
        Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region);
    }
}