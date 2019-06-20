using System.Collections.Generic;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long TypeId { get; set; }   
        public string ReadableType { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
