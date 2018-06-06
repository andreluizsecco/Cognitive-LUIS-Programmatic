namespace Cognitive.LUIS.Programmatic.Models
{
    public class EndpointInfo
    {
        public string VersionId { get; set; }
        public bool IsStaging { get; set; }
        public string EndpointUrl { get; set; }
        public string EndpointRegion { get; set; }
        public string AssignedEndpointKey { get; set; }
        public string PublishedDateTime { get; set; }
    }
}