using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IIntentService
    {
        Task<IReadOnlyCollection<Intent>> GetAllIntentsAsync(string appId, string appVersionId, int skip, int take);
        Task<Intent> GetIntentByIdAsync(string id, string appId, string appVersionId);
        Task<Intent> GetIntentByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddIntentAsync(string name, string appId, string appVersionId);
        Task RenameIntentAsync(string id, string name, string appId, string appVersionId);
        Task DeleteIntentAsync(string id, string appId, string appVersionId, bool deleteUtterances = false);
    }
}