using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface IExampleService
    {
        Task<IReadOnlyCollection<ReviewExample>> GetAllLabeledExamplesAsync(string appId, string appVersionId, int skip, int take);
        Task<Utterance> AddExampleAsync(string appId, string appVersionId, Example model);
        Task<BatchExample[]> AddBatchExampleAsync(string appId, string appVersionId, Example[] model);
        Task DeleteExampleAsync(string appId, string appVersionId, string exampleId);
    }
}