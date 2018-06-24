using System;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class LuisApp
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Culture { get; set; }
        public string UsageScenario { get; set; }
        public string Domain { get; set; }
        public int VersionsCount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Endpoint Endpoints { get; set; }
        public int EndpointHitsCount { get; set; }        
    }
}
