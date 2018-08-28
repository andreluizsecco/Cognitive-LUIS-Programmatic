using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class AppVersion
    {
        public string Version { get; set; }

        public DateTime CreatedDateTime { get; set; }
        
        public DateTime LastModifiedDateTime { get; set; }
        
        public DateTime? LastTrainedDateTime { get; set; }
        
        public DateTime? LastPublishedDateTime { get; set; }
        
        public string EndpointUrl { get; set; }
        
        public AssignedEndpointKey AssignedEndpointKey { get; set; }
        
        // public ExternalApiKeys externalApiKeys { get; set; }
        
        public int IntentsCount { get; set; }
        
        public int EntitiesCount { get; set; }
        
        public int EndpointHitsCount { get; set; }
        
        /// <summary>
        /// "NeedsTraining", "InProgress", "Trained"
        /// </summary>
        public string TrainingStatus { get; set; }
    }
}
