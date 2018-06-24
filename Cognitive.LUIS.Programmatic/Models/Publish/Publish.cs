using Newtonsoft.Json;
using System;
 
 namespace Cognitive.LUIS.Programmatic.Models
 {
     public class Publish
     {
        public string EndpointUrl { get; set; }
        public string versionId { get; set; }
        
        public string Region { get; set; }
 
        [Obsolete]
        [JsonProperty("subscription-key")]
        public string SubscriptionKey { get; set; }
        public string EndpointRegion { get; set; }
        public bool IsStaging { get; set; }
        public string AssignedEndpointKey { get; set; }
        public DateTime PublishedDateTime { get; set; }
     }
 }