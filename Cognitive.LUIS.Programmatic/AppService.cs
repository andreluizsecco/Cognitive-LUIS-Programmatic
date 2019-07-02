using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic.Apps
{
    public class AppService : ServiceClient, IAppService
    {
        public AppService(string subscriptionKey, 
                          Regions region, 
                          RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(subscriptionKey, region, retryPolicyConfiguration) { }

        /// <summary>
        /// Lists all of the user applications
        /// </summary>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of LUIS apps</returns>
        public async Task<IReadOnlyCollection<LuisApp>> GetAllAsync(int skip = 0, int take = 100)
        {
            IReadOnlyCollection<LuisApp> apps = Array.Empty<LuisApp>();
            var response = await Get($"apps?skip={skip}&take={take}");
            if (response != null)
                apps = JsonConvert.DeserializeObject<IReadOnlyCollection<LuisApp>>(response);
            return apps;
        }

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns>LUIS app</returns>
        public async Task<LuisApp> GetByIdAsync(string id)
        {
            var response = await Get($"apps/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<LuisApp>(response);
            return null;
        }

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="name">app name</param>
        /// <returns>LUIS app</returns>
        public async Task<LuisApp> GetByNameAsync(string name)
        {
            var apps = await GetAllAsync();
            return apps.FirstOrDefault(app => app.Name.Equals(name));
        }

        /// <summary>
        /// Creates a new LUIS app and returns the id
        /// </summary>
        /// <param name="name">app name</param>
        /// <param name="description">app description</param>
        /// <param name="culture">app culture: 'en-us', 'es-es', 'pt-br' and others</param>
        /// <param name="usageScenario"></param>
        /// <param name="domain"></param>
        /// <param name="initialVersionId"></param>
        /// <returns>The ID of the created app</returns>
        public async Task<string> AddAsync(string name, string description, string culture, string usageScenario, string domain, string initialVersionId)
        {
            var app = new
            {
                name,
                description,
                culture,
                usageScenario,
                domain,
                initialVersionId
            };
            var response = await Post($"apps", app);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Change the name and description of LUIS app
        /// </summary>
        /// <param name="id">app id</param>
        /// <param name="name">new app name</param>
        /// <param name="description">new app description</param>
        /// <returns></returns>
        public async Task RenameAsync(string id, string name, string description)
        {
            var app = new
            {
                name,
                description
            };
            await Put($"apps/{id}", app);
        }

        /// <summary>
        /// Deletes an application
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            await Delete($"apps/{id}");
        }
    }
}