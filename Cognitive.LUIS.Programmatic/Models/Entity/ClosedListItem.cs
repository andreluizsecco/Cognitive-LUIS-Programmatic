using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class ClosedListItem
    {
        public int Id { get; set; }
        public string CanonicalForm { get; set; }
        public IEnumerable<string> List { get; set; }
    }
}