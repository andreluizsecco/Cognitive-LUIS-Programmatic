using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cognitive.LUIS.Programmatic.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected const string SubscriptionKey = "{YourSubscriptionKey}";
        protected const Regions Region = Regions.WestUS;
        protected const string InvalidId = "51593248-363e-4a08-b946-2061964dc690";
        protected const string appVersion = "1.0";
        protected static string appId;

        [AssemblyInitialize]
        public static void Initialize(TestContext context) =>
            CreateApp();

        protected static void CreateApp()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                appId = app.Id;
            else
                appId = client.AddAppAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion).Result;
        }
        
        [AssemblyCleanup]
        public static void Cleanup()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = client.GetAppByNameAsync("SDKTest").Result;
            if (app != null)
                client.DeleteAppAsync(app.Id).Wait();
            appId = null;
        }
    }
}