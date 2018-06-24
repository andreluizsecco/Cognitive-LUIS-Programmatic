using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class Example
    {
        public string Text { get; set; }
        public string IntentName { get; set; }
        public IEnumerable<EntityLabel> EntityLabels { get; set; }
    }
}
