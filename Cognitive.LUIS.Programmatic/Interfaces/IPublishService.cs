using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IPublishService
    {
        Task<Publish> PublishAsync(string appId, string appVersionId, bool isStaging, string region);
    }
}