using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Versions
{
    public interface IVersionService : IDisposable
    {
        Task<IReadOnlyCollection<AppVersion>> GetAllAsync(string appId, int skip = 0, int take = 100);
        Task<AppVersion> GetByIdAsync(string appId, string versionId);
    }
}
