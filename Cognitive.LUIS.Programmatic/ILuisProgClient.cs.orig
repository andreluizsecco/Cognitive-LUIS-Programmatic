using Cognitive.LUIS.Programmatic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public interface ILuisProgClient
    {
        Task<IReadOnlyCollection<LuisApp>> GetAllAppsAsync();
        Task<LuisApp> GetAppByIdAsync(string id);
        Task<LuisApp> GetAppByNameAsync(string name);
        Task<string> AddAppAsync(string name, string description, string culture, string usageScenario, string domain, string initialVersionId);
        Task RenameAppAsync(string id, string name, string description);
        Task DeleteAppAsync(string id);

        Task<IReadOnlyCollection<Intent>> GetAllIntentsAsync(string appId, string appVersionId);
        Task<Intent> GetIntentByIdAsync(string id, string appId, string appVersionId);
        Task<Intent> GetIntentByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddIntentAsync(string name, string appId, string appVersionId);
        Task RenameIntentAsync(string id, string name, string appId, string appVersionId);
        Task DeleteIntentAsync(string id, string appId, string appVersionId);

        Task<IReadOnlyCollection<Entity>> GetAllEntitiesAsync(string appId, string appVersionId);
        Task<Entity> GetEntityByIdAsync(string id, string appId, string appVersionId);
        Task<Entity> GetEntityByNameAsync(string name, string appId, string appVersionId);
        Task<string> AddEntityAsync(string name, string appId, string appVersionId);
        Task RenameEntityAsync(string id, string name, string appId, string appVersionId);
        Task DeleteEntityAsync(string id, string appId, string appVersionId);

        Task<Utterance> AddExampleAsync(string appId, string appVersionId, Example model);

        Task<TrainingDetails> TrainAsync(string appId, string appVersionId);
        Task<IEnumerable<Training>> GetTrainingStatusListAsync(string appId, string appVersionId);

        Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region);
    }
}
