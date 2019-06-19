using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Entities
{
    public interface IEntityService : IDisposable
    {
        Task<IReadOnlyCollection<Entity>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100);
        Task<Entity> GetByIdAsync(string id, string appId, string appVersionId);
        Task<Entity> GetByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddAsync(string name, string appId, string appVersionId);
        Task RenameAsync(string id, string name, string appId, string appVersionId);
        Task DeleteAsync(string id, string appId, string appVersionId);
    }
}