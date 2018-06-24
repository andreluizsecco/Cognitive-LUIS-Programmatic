namespace Cognitive.LUIS.Programmatic.Models
{
    public class BatchExample
    {
        public Utterance Value { get; set; }
        public bool HasError { get; set; }
        public Error Error { get; set; }
    }    
}