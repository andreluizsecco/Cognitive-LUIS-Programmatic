using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public class BaseTest
    {
        protected const string SubscriptionKey = "{YourSubscriptionKey}";
        protected const Regions Region = Regions.WestUS;
        protected const string InvalidId = "51593248-363e-4a08-b946-2061964dc690";
        protected const string appVersion = "1.0";
        protected string appId;

        protected async Task CreateApp()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = await client.GetAppByNameAsync("SDKTest");
            if (app != null)
                appId = app.Id;
            else
                appId = await client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion);
        }

        protected async Task Cleanup()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = await client.GetAppByNameAsync("SDKTest");
            if (app != null)
                await client.DeleteAppAsync(app.Id);
            appId = null;
        }

        ~BaseTest() => Cleanup().Wait();
    }
}