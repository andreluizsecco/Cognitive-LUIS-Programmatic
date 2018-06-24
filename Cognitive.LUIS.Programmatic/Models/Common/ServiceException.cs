namespace Cognitive.LUIS.Programmatic.Models
{
    public class ServiceException
    {
        public Error Error { get; set; }
        public string Message { get; set; }

        public override string ToString() =>
            $"{Error?.Code ?? "Unexpected"} - {Error?.Message ?? Message}";
    }
}