using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IAppService
    {
        /// <summary>
        /// Lists all of the user applications
        /// </summary>
        /// <returns>A List of LUIS apps</returns>
        public async Task<IReadOnlyCollection<LuisApp>> GetAllAppsAsync()
        {
            var response = await Get($"/apps");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IReadOnlyCollection<LuisApp>>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{exception.Error.Code} - {exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns>LUIS app</returns>
        public async Task<LuisApp> GetAppByIdAsync(string id)
        {
            var response = await Get($"/apps/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<LuisApp>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{exception.Error.Code} - {exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="name">app name</param>
        /// <returns>LUIS app</returns>
        public async Task<LuisApp> GetAppByNameAsync(string name)
        {
            var apps = await GetAllAppsAsync();
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
        public async Task<string> AddAppAsync(string name, string description, string culture, string usageScenario, string domain, string initialVersionId)
        {
            var app = new
            {
                name = name,
                description = description,
                culture = culture,
                usageScenario = usageScenario,
                domain = domain,
                initialVersionId = initialVersionId
            };
            var response = await Post($"/apps", app);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Change the name and description of LUIS app
        /// </summary>
        /// <param name="id">app id</param>
        /// <param name="name">new app name</param>
        /// <param name="description">new app description</param>
        /// <returns></returns>
        public async Task RenameAppAsync(string id, string name, string description)
        {
            var app = new
            {
                name = name,
                description = description
            };
            await Put($"/apps/{id}", app);
        }

        /// <summary>
        /// Deletes an application
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns></returns>
        public async Task DeleteAppAsync(string id)
        {
            await Delete($"/apps/{id}");
        }
    }
}