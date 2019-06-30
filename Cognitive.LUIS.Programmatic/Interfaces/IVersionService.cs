using Cognitive.LUIS.Programmatic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Versions
{
    public interface IVersionService : IDisposable
    {
        /// <summary>
        /// Gets the application versions info
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="skip">the number of entries to skip. Default value is 0</param>
        /// <param name="take">the number of entries to return. Maximum page size is 500. Default is 100</param>
        /// <returns>A List of app versions</returns>
        Task<IReadOnlyCollection<AppVersion>> GetAllAsync(string appId, int skip = 0, int take = 100);

        /// <summary>
        /// Gets the task info
        /// </summary>
        /// <param name="appId">app id</param>
        /// <param name="versionId">app version</param>
        /// <returns>app version</returns>
        Task<AppVersion> GetByIdAsync(string appId, string versionId);
    }
}
