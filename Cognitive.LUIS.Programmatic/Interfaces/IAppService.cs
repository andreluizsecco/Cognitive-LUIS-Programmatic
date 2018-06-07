using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IAppService
    {
        Task<IReadOnlyCollection<LuisApp>> GetAllAppsAsync();
        Task<LuisApp> GetAppByIdAsync(string id);
        Task<LuisApp> GetAppByNameAsync(string name);
        Task<string> AddAppAsync(string name, string description, string culture, string usageScenario, string domain, string initialVersionId);
        Task RenameAppAsync(string id, string name, string description);
        Task DeleteAppAsync(string id);
    }
}