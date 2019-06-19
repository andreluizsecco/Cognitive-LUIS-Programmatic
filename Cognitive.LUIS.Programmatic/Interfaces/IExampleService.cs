using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Examples
{
    public interface IExampleService : IDisposable
    {
        Task<IReadOnlyCollection<ReviewExample>> GetAllAsync(string appId, string appVersionId, int skip = 0, int take = 100);
        Task<Utterance> AddAsync(string appId, string appVersionId, Example model);
        Task<BatchExample[]> AddBatchAsync(string appId, string appVersionId, Example[] model);
        Task DeleteAsync(string appId, string appVersionId, string exampleId);
    }
}