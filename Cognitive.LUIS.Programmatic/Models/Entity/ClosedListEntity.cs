using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class ClosedListEntity : Entity
    {
        public IEnumerable<ClosedListItem> SubLists { get; set; }
    }
}
