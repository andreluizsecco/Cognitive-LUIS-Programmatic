using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic.Training
{
    public interface ITrainingService : IDisposable
    {
        Task<TrainingDetails> TrainAsync(string appId, string appVersionId);
        Task<IReadOnlyCollection<Models.Training>> GetStatusListAsync(string appId, string appVersionId);
        Task<TrainingDetails> TrainAndGetFinalStatusAsync(string appId, string appVersionId, int timeoutt = 60);
    }
}