using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IPublishService
    {
        /// <summary>
        /// Publishes a specific version of the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="isStaging"></param>
        /// <param name="region">The publish location is determined from the creation location. To publish to more than one region, the region properties should be a comma-separated list, "region": "westus, westeurope".</param>
        /// <returns>A object of publish details</returns>
        public async Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region)
        {
            var model = new
            {
                versionId = appVersionId,
                isStaging = isStaging.ToString(),
                region = region
            };
            var response = await Post($"apps/{appId}/publish", model);
            return JsonConvert.DeserializeObject<Publish>(response);
        }
    }
}