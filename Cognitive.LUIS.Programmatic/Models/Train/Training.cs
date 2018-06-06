using Newtonsoft.Json;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class Training
    {
        public string ModelId { get; set; }

        [JsonProperty("details")]
        public TrainingDetails Details { get; set; }
    }
}
