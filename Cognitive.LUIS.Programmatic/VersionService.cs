using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IVersionService
    {
        public async Task<IReadOnlyCollection<AppVersion>> GetAllVersionsAsync(string appId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<AppVersion> apps = new List<AppVersion>();
            var response = await Get($"apps/{appId}/versions?skip={skip}&take={take}");
            if (response != null)
                apps = JsonConvert.DeserializeObject<IReadOnlyCollection<AppVersion>>(response);

            return apps;
        }

        public async Task<AppVersion> GetVersionAsync(string appId, string versionId)
        {
            var response = await Get($"apps/{appId}/versions/{versionId}/");
            if (response != null)
                return JsonConvert.DeserializeObject<AppVersion>(response);

            return null;
        }
    }
}
