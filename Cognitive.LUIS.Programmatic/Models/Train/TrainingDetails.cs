using System;

namespace Cognitive.LUIS.Programmatic.Models
{
    public class TrainingDetails
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int ExampleCount { get; set; }
        public DateTime? TrainingDateTime { get; set; }
        public string FailureReason { get; set; }
    }
}