using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class CompositeEntity : Entity
    {
        public IEnumerable<CompositeChild> Children { get; set; }
    }
}
