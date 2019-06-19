using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Intents
{
    public interface IIntentService : IDisposable
    {
        Task<IReadOnlyCollection<Intent>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100);
        Task<Intent> GetByIdAsync(string id, string appId, string appVersionId);
        Task<Intent> GetByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddAsync(string name, string appId, string appVersionId);
        Task RenameAsync(string id, string name, string appId, string appVersionId);
        Task DeleteAsync(string id, string appId, string appVersionId, bool deleteUtterances = false);
    }
}