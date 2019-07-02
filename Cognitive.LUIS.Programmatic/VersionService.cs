using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Versions
{
    public class VersionService : ServiceClient, IVersionService
    {
        public VersionService(string subscriptionKey, Regions region, RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(subscriptionKey, region, retryPolicyConfiguration) { }

        /// <summary>
        /// Gets the application versions info
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of app versions</returns>
        public async Task<IReadOnlyCollection<AppVersion>> GetAllAsync(string appId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<AppVersion> apps = new List<AppVersion>();
            var response = await Get($"apps/{appId}/versions?skip={skip}&take={take}");
            if (response != null)
                apps = JsonConvert.DeserializeObject<IReadOnlyCollection<AppVersion>>(response);

            return apps;
        }
        
        /// <summary>
        /// Gets the task info
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="versionId">app version</param>
        /// <returns>app version</returns>
        public async Task<AppVersion> GetByIdAsync(string appId, string versionId)
        {
            var response = await Get($"apps/{appId}/versions/{versionId}/");
            if (response != null)
                return JsonConvert.DeserializeObject<AppVersion>(response);

            return null;
        }
    }
}
