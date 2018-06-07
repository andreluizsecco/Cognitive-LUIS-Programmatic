using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IEntityService
    {
        Task<IReadOnlyCollection<Entity>> GetAllEntitiesAsync(string appId, string appVersionId);
        Task<Entity> GetEntityByIdAsync(string id, string appId, string appVersionId);
        Task<Entity> GetEntityByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddEntityAsync(string name, string appId, string appVersionId);
        Task RenameEntityAsync(string id, string name, string appId, string appVersionId);
        Task DeleteEntityAsync(string id, string appId, string appVersionId);
    }
}