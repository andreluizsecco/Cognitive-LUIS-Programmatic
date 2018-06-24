using System;
using System.Net;
using System.Net.Http;

namespace Cognitive.LUIS.Programmatic
{
    public static class HttpClientFactory
    {
        public static HttpClient Create(string baseUrl, string subscriptionKey)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            client.DefaultRequestHeaders.ConnectionClose = false;
            ServicePointManager.FindServicePoint(client.BaseAddress).ConnectionLeaseTimeout = 60*1000; //1 minute
            return client;
        }
    }
}