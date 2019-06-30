using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Entities
{
    public interface IEntityService : IDisposable
    {
        /// <summary>
        /// Gets information about the simple entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of simple entities</returns>
        Task<IReadOnlyCollection<Entity>> GetAllSimpleEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the composite entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of composite entities</returns>
        Task<IReadOnlyCollection<CompositeEntity>> GetAllCompositeEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the closed list entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of closed list entities</returns>
        Task<IReadOnlyCollection<ClosedListEntity>> GetAllClosedListEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the regex entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of regex entities</returns>
        Task<IReadOnlyCollection<RegexEntity>> GetAllRegexEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the PatternAny entity models
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of PatternAny entities</returns>
        Task<IReadOnlyCollection<PatternAnyEntity>> GetAllPatternAnyEntitiesAsync(string appId, string appVersionId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets information about the simple entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>simple entity</returns>
        Task<Entity> GetSimpleEntityByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the composite entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>composite entity</returns>
        Task<CompositeEntity> GetCompositeEntityByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the closed list entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>closed list entity</returns>
        Task<ClosedListEntity> GetClosedListEntityByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the regex entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>regex entity</returns>
        Task<RegexEntity> GetRegexEntityByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the PatternAny entity model
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>PatterAny entity</returns>
        Task<PatternAnyEntity> GetPatternAnyEntityByIdAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the simple entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>simple entity</returns>
        Task<Entity> GetSimpleEntityByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the composite entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>composite entity</returns>
        Task<CompositeEntity> GetCompositeEntityByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the closed list entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>closed list entity</returns>
        Task<ClosedListEntity> GetClosedListEntityByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the regex entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>regex entity</returns>
        Task<RegexEntity> GetRegexEntityByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Gets information about the PatternAny entity model
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>PatternAny entity</returns>
        Task<PatternAnyEntity> GetPatternAnyEntityByNameAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Creates a new simple entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        Task<string> AddSimpleEntityAsync(string name, string appId, string appVersionId);

        /// <summary>
        /// Creates a new composite entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="children">list of child entity names</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        Task<string> AddCompositeEntityAsync(string name, IEnumerable<string> children, string appId, string appVersionId);

        /// <summary>
        /// Creates a new closed list entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="subLists">list of inner values and synonyms</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        Task<string> AddClosedListEntityAsync(string name, IEnumerable<ClosedListItem> subLists, string appId, string appVersionId);

        /// <summary>
        /// Creates a new regex entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="regexPattern">regex pattern</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        Task<string> AddRegexEntityAsync(string name, string regexPattern, string appId, string appVersionId);

        /// <summary>
        /// Creates a new PatternAny entity and returns the id
        /// </summary>
        /// <param name="name">entity name</param>
        /// <param name="explicityList">list of inner values</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns>The ID of the created entity</returns>
        Task<string> AddPatternAnyEntityAsync(string name, IEnumerable<string> explicitList, string appId, string appVersionId);

        /// <summary>
        /// Change the name of app entity
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="name">new intent name</param>
        /// <param name="appId">app id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task RenameAsync(string id, string name, string appId, string appVersionId);

        /// <summary>
        /// Deletes an entity extractor from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task DeleteSimpleEntityAsync(string id, string appId, string appVersionId);
        
        /// <summary>
        /// Deletes a composite entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task DeleteCompositeEntityAsync(string id, string appId, string appVersionId);
        
        /// <summary>
        /// Deletes a closed list entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task DeleteClosedListEntityAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Deletes a regex entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task DeleteRegexEntityAsync(string id, string appId, string appVersionId);

        /// <summary>
        /// Deletes a PatternAny entity model from the application
        /// </summary>
        /// <param name="id">entity id</param>
        /// <param name="appId">app Id</param>
        /// <param name="appVersionId">app version</param>
        /// <returns></returns>
        Task DeletePatternAnyEntityAsync(string id, string appId, string appVersionId);
    }
}