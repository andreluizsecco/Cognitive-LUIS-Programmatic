using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class Example
    {
        public string Text { get; set; }
        public string IntentName { get; set; }
        public IEnumerable<EntityLabel> EntityLabels { get; set; }
    }

    public class EntityLabel
    {
        public string EntityName { get; set; }
        public int StartCharIndex { get; set; }
        public int EndCharIndex { get; set; }
    }

    public class LabeledExample
    {
        public string Text { get; set; }
        public string IntentLabel { get; set; }
        public string[] TokenizedText { get; set; }
        public IEnumerable<EntityLabel> EntityLabels { get; set; }
        public IEnumerable<IntentPrediction> IntentPredictions { get; set; }
    }

    public class IntentPrediction
    {
        public string Name { get; set; }
        public double? Score { get; set; }
    }
}
