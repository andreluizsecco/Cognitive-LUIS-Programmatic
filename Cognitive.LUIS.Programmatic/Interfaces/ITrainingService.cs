using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface ITrainingService
    {
        Task<TrainingDetails> TrainAsync(string appId, string appVersionId);
        Task<IReadOnlyCollection<Training>> GetTrainingStatusListAsync(string appId, string appVersionId);
        Task<TrainingDetails> TrainAndGetFinalStatusAsync(string appId, string appVersionId, int timeout);
    }
}