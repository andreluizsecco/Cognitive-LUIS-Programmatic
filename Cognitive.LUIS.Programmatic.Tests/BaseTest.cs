using System;

namespace Cognitive.LUIS.Programmatic.Tests
{
    public abstract class BaseTest : IDisposable
    {
        protected const string SubscriptionKey = "{YourSubscriptionKey}";
        protected const Regions Region = Regions.WestUS;
        protected const string InvalidId = "51593248-363e-4a08-b946-2061964dc690";
        protected const string appVersion = "1.0";
        protected static string appId;

        protected void Initialize()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = client.Apps.GetByNameAsync("SDKTest").Result;
            if (app != null)
                appId = app.Id;
            else
                appId = client.Apps.AddAsync("SDKTest", "Description test", "en-us", "SDKTest", string.Empty, appVersion).Result;
        }
        
        protected void Cleanup()
        {
            var client = new LuisProgClient(SubscriptionKey, Region);
            var app = client.Apps.GetByNameAsync("SDKTest").Result;
            if (app != null)
                client.Apps.DeleteAsync(app.Id).Wait();
            app = client.Apps.GetByNameAsync("SDKTestChanged").Result;
            if (app != null)
                client.Apps.DeleteAsync(app.Id).Wait();
            appId = null;
        }

        public abstract void Dispose();
    }
}