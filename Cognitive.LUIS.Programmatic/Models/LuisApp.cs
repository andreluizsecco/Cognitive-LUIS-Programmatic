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
        public Endpoints Endpoints { get; set; }
        public int EndpointHitsCount { get; set; }        
    }

    public class Endpoints
    {
        public Production Production { get; set; }
    }

    public class Production
    {
        public string VersionId { get; set; }
        public bool IsStaging { get; set; }
        public string EndpointUrl { get; set; }
        public string EndpointRegion { get; set; }
        public string AssignedEndpointKey { get; set; }
        public string PublishedDateTime { get; set; }
    }
}
