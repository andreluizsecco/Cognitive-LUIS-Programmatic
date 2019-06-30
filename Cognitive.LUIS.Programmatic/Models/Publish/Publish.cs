using Newtonsoft.Json;
using System;
 
 namespace Cognitive.LUIS.Programmatic.Models
 {
     public class Publish
     {
        public string VersionId { get; set; }
        public bool DirectVersionPublish { get; set; }
        public string EndpointUrl { get; set; }
        public bool IsStaging { get; set; }
        public string AssignedEndpointKey { get; set; }
        public string Region { get; set; }
        public string EndpointRegion { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string FailedRegions { get; set; }
    }
 }