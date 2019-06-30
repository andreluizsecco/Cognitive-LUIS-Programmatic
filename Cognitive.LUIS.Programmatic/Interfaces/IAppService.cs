using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Apps
{
    public interface IAppService : IDisposable
    {
        /// <summary>
        /// Lists all of the user applications
        /// </summary>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of LUIS apps</returns>
        Task<IReadOnlyCollection<LuisApp>> GetAllAsync(int skip = 0, int take = 100);

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns>LUIS app</returns>
        Task<LuisApp> GetByIdAsync(string id);

        /// <summary>
        /// Gets the application info
        /// </summary>
        /// <param name="name">app name</param>
        /// <returns>LUIS app</returns>
        Task<LuisApp> GetByNameAsync(string name);

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
        Task<string> AddAsync(string name, string description, string culture, string usageScenario, string domain, string initialVersionId);

        /// <summary>
        /// Change the name and description of LUIS app
        /// </summary>
        /// <param name="id">app id</param>
        /// <param name="name">new app name</param>
        /// <param name="description">new app description</param>
        /// <returns></returns>
        Task RenameAsync(string id, string name, string description);

        /// <summary>
        /// Deletes an application
        /// </summary>
        /// <param name="id">app id</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}