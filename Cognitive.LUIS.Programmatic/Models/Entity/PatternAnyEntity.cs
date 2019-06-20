using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class PatternAnyEntity : Entity
    {
        public IEnumerable<PatternAnyItem> ExplicityList { get; set; }
    }
}
