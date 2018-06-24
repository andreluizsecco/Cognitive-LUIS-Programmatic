using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class ReviewExample
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<string> TokenizedText { get; set; }
        public string IntentLabel { get; set; }
        public IEnumerable<ReviewEntityLabel> EntityLabels { get; set; }
        public IEnumerable<IntentPrediction> IntentPredictions { get; set; }
        public IEnumerable<EntityPrediction> EntityPredictions { get; set; }
    }
}