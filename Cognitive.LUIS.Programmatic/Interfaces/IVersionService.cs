using Cognitive.LUIS.Programmatic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public interface IVersionService
    {
        Task<IReadOnlyCollection<AppVersion>> GetAllVersionsAsync(string appId, int skip = 0, int take = 100);

        Task<AppVersion> GetVersionAsync(string appId, string versionId);
    }
}
