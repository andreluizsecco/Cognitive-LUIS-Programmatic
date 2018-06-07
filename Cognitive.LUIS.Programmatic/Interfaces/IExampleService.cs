using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IExampleService
    {
        Task<Utterance> AddExampleAsync(string appId, string appVersionId, Example model);
        Task<BatchExample[]> AddBatchExampleAsync(string appId, string appVersionId, Example[] model);
        Task DeleteExampleAsync(string appId, string appVersionId, string exampleId);
    }
}