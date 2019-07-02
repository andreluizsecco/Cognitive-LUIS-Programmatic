using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic.Entities
{
    public class EntityService : ServiceClient, IEntityService
    {
        public EntityService(string subscriptionKey, Regions region, RetryPolicyConfiguration retryPolicyConfiguration = null)
            : base(subscriptionKey, region, retryPolicyConfiguration) { }

        /// <summary>
        /// Gets information about the simple entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of simple entities</returns>
        public async Task<IReadOnlyCollection<Entity>> GetAllSimpleEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<Entity> entities = Array.Empty<Entity>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/entities?skip={skip}&take={take}");
            if (response != null)
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<Entity>>(response);
            return entities;
        }

        /// <summary>
        /// Gets information about the composite entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of composite entities</returns>
        public async Task<IReadOnlyCollection<CompositeEntity>> GetAllCompositeEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<CompositeEntity> entities = Array.Empty<CompositeEntity>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/compositeentities?skip={skip}&take={take}");
            if (response != null)
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<CompositeEntity>>(response);
            return entities;
        }

        /// <summary>
        /// Gets information about the closed list entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of closed list entities</returns>
        public async Task<IReadOnlyCollection<ClosedListEntity>> GetAllClosedListEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<ClosedListEntity> entities = Array.Empty<ClosedListEntity>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/closedlists?skip={skip}&take={take}");
            if (response != null)
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<ClosedListEntity>>(response);
            return entities;
        }

        /// <summary>
        /// Gets information about the regex entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of regex entities</returns>
        public async Task<IReadOnlyCollection<RegexEntity>> GetAllRegexEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<RegexEntity> entities = Array.Empty<RegexEntity>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/regexentities?skip={skip}&take={take}");
            if (response != null)
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<RegexEntity>>(response);
            return entities;
        }

        /// <summary>
        /// Gets information about the PatternAny entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of PatternAny entities</returns>
        public async Task<IReadOnlyCollection<PatternAnyEntity>> GetAllPatternAnyEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100)
        {
            IReadOnlyCollection<PatternAnyEntity> entities = Array.Empty<PatternAnyEntity>();
            var response = await Get($"apps/{appId}/versions/{appVersionId}/patternanyentities?skip={skip}&take={take}");
            if (response != null)
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<PatternAnyEntity>>(response);
            return entities;
        }

        /// <summary>
        /// Gets information about the entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app entity</returns>
        public async Task<Entity> GetSimpleEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/entities/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<Entity>(response);
            return null;
        }

        /// <summary>
        /// Gets information about the composite entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>composite entity</returns>
        public async Task<CompositeEntity> GetCompositeEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/compositeentities/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<CompositeEntity>(response);
            return null;
        }

        /// <summary>
        /// Gets information about the closed list entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>closed list entity</returns>
        public async Task<ClosedListEntity> GetClosedListEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/closedlists/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<ClosedListEntity>(response);
            return null;
        }

        /// <summary>
        /// Gets information about the regex entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>regex entity</returns>
        public async Task<RegexEntity> GetRegexEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/regexentities/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<RegexEntity>(response);
            return null;
        }

        /// <summary>
        /// Gets information about the PatternAny entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>PatternAny entity</returns>
        public async Task<PatternAnyEntity> GetPatternAnyEntityByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"apps/{appId}/versions/{appVersionId}/patternanyentities/{id}");
            if (response != null)
                return JsonConvert.DeserializeObject<PatternAnyEntity>(response);
            return null;
        }

        /// <summary>
        /// Gets information about the simple entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>simple entity</returns>
        public async Task<Entity> GetSimpleEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllSimpleEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Gets information about the composite entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>composite entity</returns>
        public async Task<CompositeEntity> GetCompositeEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllCompositeEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Gets information about the closed list entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>closed list entity</returns>
        public async Task<ClosedListEntity> GetClosedListEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllClosedListEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Gets information about the regex entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>regex entity</returns>
        public async Task<RegexEntity> GetRegexEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllRegexEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Gets information about the PatternAny entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>PatternAny entity</returns>
        public async Task<PatternAnyEntity> GetPatternAnyEntityByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllPatternAnyEntitiesAsync(appId, appVersionId);
            return apps.FirstOrDefault(Entity => Entity.Name.Equals(name));
        }

        /// <summary>
        /// Creates a new simple entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddSimpleEntityAsync(string name, string appId, string appVersionId)
        {
            var entity = new
            {
                name
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/entities", entity);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Creates a new composite entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="children">list of child entity names</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddCompositeEntityAsync(string name, IEnumerable<string> children, string appId, string appVersionId)
        {
            var entity = new
            {
                name,
                children
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/compositeentities", entity);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Creates a new closed list entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="subLists">list of inner values and synonyms</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddClosedListEntityAsync(string name, IEnumerable<ClosedListItem> subLists, string appId, string appVersionId)
        {
            var entity = new
            {
                name,
                sublists = subLists
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/closedlists", entity);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Creates a new regex entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="regexPattern">regex pattern</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddRegexEntityAsync(string name, string regexPattern, string appId, string appVersionId)
        {
            var entity = new
            {
                name,
                regexPattern
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/regexentities", entity);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Creates a new PatternAny entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="explicityList">list of inner values</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        public async Task<string> AddPatternAnyEntityAsync(string name, IEnumerable<string> explicitList, string appId, string appVersionId)
        {
            var entity = new
            {
                name,
                explicitList
            };
            var response = await Post($"apps/{appId}/versions/{appVersionId}/patternanyentities", entity);
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
        public async Task RenameAsync(string id, string name, string appId, string appVersionId)
        {
            var entity = new
            {
                name
            };
            await Put($"apps/{appId}/versions/{appVersionId}/entities/{id}", entity);
        }

        /// <summary>
        /// Deletes an entity extractor from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteSimpleEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/entities/{id}");
        }

        /// <summary>
        /// Deletes a composite entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteCompositeEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/compositeentities/{id}");
        }

        /// <summary>
        /// Deletes a closed list entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteClosedListEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/closedlists/{id}");
        }

        /// <summary>
        /// Deletes a regex entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteRegexEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/regexentities/{id}");
        }

        /// <summary>
        /// Deletes a composite entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeletePatternAnyEntityAsync(string id, string appId, string appVersionId)
        {
            await Delete($"apps/{appId}/versions/{appVersionId}/patternanyentities/{id}");
        }
    }
}