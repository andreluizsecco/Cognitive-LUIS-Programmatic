using System;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IPublishService : IDisposable
    {
        Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region);
    }
}