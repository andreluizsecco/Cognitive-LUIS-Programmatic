using System.Collections.Generic;
using System.Threading.Tasks;
using Cognitive.LUIS.Programmatic.Models;

namespace Cognitive.LUIS.Programmatic
{
    public interface ITrainService
    {
        Task<TrainingDetails> TrainAsync(string appId, string appVersionId);
        Task<TrainingDetails> TrainAndGetCompletionStatusAsync(string appId, string appVersionId);
        Task<IEnumerable<Training>> GetTrainingStatusListAsync(string appId, string appVersionId);
    }
}