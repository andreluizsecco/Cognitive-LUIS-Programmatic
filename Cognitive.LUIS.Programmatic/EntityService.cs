using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : IEntityService
    {
        /// <summary>
        /// Gets information about the entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A List of app entities</returns>
        public async Task<IReadOnlyCollection<Entity>> GetAllEntitiesAsync(string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/entities");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IReadOnlyCollection<Entity>>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets information about the entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app entity</returns>
        public async Task<Entity> GetEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/entities/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Entity>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets information about the entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app entity</returns>
        public async Task<Entity> GetEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Creates a new app entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddEntityAsync(string name, string appId, string appVersionId)
        {
            var Entity = new
            {
                name = name
            };
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/entities", Entity);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Change the name of app entity
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="name">new intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task RenameEntityAsync(string id, string name, string appId, string appVersionId)
        {
            var entity = new
            {
                name = name
            };
            await Put($"/apps/{appId}/versions/{appVersionId}/entities/{id}", entity);
        }

        /// <summary>
        /// Deletes an entity extractor from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"/apps/{appId}/versions/{appVersionId}/entities/{id}");
        }
    }
}