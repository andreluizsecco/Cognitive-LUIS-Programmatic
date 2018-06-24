namespace Cognitive.LUIS.Programmatic
{
    public partial class LuisProgClient : ServiceClient
    {
        /// <param name="subscriptionKey">LUIS Authoring Key</param>
        /// <param name="region">Regions currently available in West US, West Europe and Australia East".</param>
        public LuisProgClient(string subscriptionKey, Regions region) : base(subscriptionKey, region) { }
    }
}
