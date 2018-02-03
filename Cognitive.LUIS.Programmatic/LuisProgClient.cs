using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public class LuisProgClient : ServiceClient, ILuisProgClient
    {
        public LuisProgClient(string subscriptionKey, Location location) : base(subscriptionKey, location) { }

        #region Apps

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

        #endregion

        #region Intents

        /// <summary>
        /// Gets information about the intent models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A List of app intents</returns>
        public async Task<IReadOnlyCollection<Intent>> GetAllIntentsAsync(string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/intents");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IReadOnlyCollection<Intent>>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        public async Task<Intent> GetIntentByIdAsync(string id, string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/intents/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Intent>(content);
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
            return null;
        }

        /// <summary>
        /// Gets information about the intent model
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>app intent</returns>
        public async Task<Intent> GetIntentByNameAsync(string name, string appId, string appVersionId)
        {
            var apps = await GetAllIntentsAsync(appId, appVersionId);
            return apps.FirstOrDefault(intent => intent.Name.Equals(name));
        }

        /// <summary>
        /// Creates a new app intent and returns the id
        /// </summary>
        /// <param name="name">intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created intent</returns>
        public async Task<string> AddIntentAsync(string name, string appId, string appVersionId)
        {
            var intent = new
            {
                name = name
            };
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/intents", intent);
            return JsonConvert.DeserializeObject<string>(response);
        }

        /// <summary>
        /// Change the name of app intent
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="name">new intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task RenameIntentAsync(string id, string name, string appId, string appVersionId)
        {
            var intent = new
            {
                name = name
            };
            await Put($"/apps/{appId}/versions/{appVersionId}/intents/{id}", intent);
        }

        /// <summary>
        /// Deletes an intent classifier from the application
        /// </summary>
        /// <param name="id">intent id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        public async Task DeleteIntentAsync(string id, string appId, string appVersionId)
        {
            await Delete($"/apps/{appId}/versions/{appVersionId}/intents/{id}");
        }

        #endregion

        #region Entities

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

        #endregion

        #region examples

        /// <summary>
        /// Adds a labeled example to the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="model">object containing the example label</param>
        /// <returns>A object of utterance created</returns>
        public async Task<Utterance> AddExampleAsync(string appId, string appVersionId, Example model)
        {
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/example", model);
            return JsonConvert.DeserializeObject<Utterance>(response);
        }

        #endregion

        #region Train

        /// <summary>
        /// Sends a training request for a version of a specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A object of training request details</returns>
        public async Task<TrainingDetails> TrainAsync(string appId, string appVersionId)
        {
            var response = await Post($"/apps/{appId}/versions/{appVersionId}/train");
            return JsonConvert.DeserializeObject<TrainingDetails>(response);
        }

        /// <summary>
        /// Gets the training status of all models (intents and entities) for the specified LUIS app
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>A list of trainings status</returns>
        public async Task<IEnumerable<Training>> GetTrainingStatusListAsync(string appId, string appVersionId)
        {
            var response = await Get($"/apps/{appId}/versions/{appVersionId}/train");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Training>>(content);
            else
            {
                var exception = JsonConvert.DeserializeObject<ServiceException>(content);
                throw new Exception($"{ exception.Error.Code} - { exception.Error.Message}");
            }
        }

        #endregion

        #region Publish

        /// <summary>
        /// Publishes a specific version of the application
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="isStaging"></param>
        /// <param name="region">If the app is created in "westeurope", then the publish location is also "westeurope." For all other app locations, the publish location is "westus"</param>
        /// <returns>A object of publish details</returns>
        public async Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region)
        {
            var model = new
            {
                versionId = appVersionId,
                isStaging = isStaging.ToString(),
                region = region
            };
            var response = await Post($"/apps/{appId}/publish", model);
            return JsonConvert.DeserializeObject<Publish>(response);
        }

        #endregion
    }
}
