namespace Cognitive.LUIS.Programmatic
{
    public class RetryPolicyConfiguration
    {
        public static RetryPolicyConfiguration Default = new RetryPolicyConfiguration(3, 1);

        public int RetryCount { get; private set; }
        public int RetryAttemptFactor { get; private set; }

        public RetryPolicyConfiguration(int retryCount, int retryAttemptFactor)
        {
            this.RetryCount = retryCount;
            this.RetryAttemptFactor = retryAttemptFactor;
        }
    }
}
