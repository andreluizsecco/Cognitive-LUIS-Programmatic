using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public class ServiceClient
    {
        private const string BASE_URL = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0";
        private readonly string _subscriptionKey;

        public ServiceClient(string subscriptionKey) =>
            _subscriptionKey = subscriptionKey;

        protected async Task<HttpResponseMessage> Get(string path)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                return await client.GetAsync($"{BASE_URL}{path}");
            }
        }

        protected async Task<string> Post(string path)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                var response = await client.PostAsync($"{BASE_URL}{path}", null);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return responseContent;
                else
                {
                    var exception = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                    throw new Exception($"{exception.Error?.Message ?? exception.Message}");
                }
            }
        }

        protected async Task<string> Post<TRequest>(string path, TRequest requestBody)
        {
            using (var client = new HttpClient())
            {
                using (var content = new ByteArrayContent(GetByteData(requestBody)))
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync($"{BASE_URL}{path}", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                        return responseContent;
                    else
                    {
                        var exception = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                        throw new Exception($"{exception.Error?.Message ?? exception.Message}");
                    }
                }
            }
        }

        protected async Task Put<TRequest>(string path, TRequest requestBody)
        {
            using (var client = new HttpClient())
            {
                using (var content = new ByteArrayContent(GetByteData(requestBody)))
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync($"{BASE_URL}{path}", content);
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var exception = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                        throw new Exception($"{exception.Error?.Message ?? exception.Message}");
                    }
                }
            }
        }

        protected async Task Delete(string path)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
                var response = await client.DeleteAsync($"{BASE_URL}{path}");
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var exception = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                    throw new Exception($"{exception.Error?.Message ?? exception.Message}");
                }
            }
        }

        private byte[] GetByteData<TRequest>(TRequest requestBody)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var body = JsonConvert.SerializeObject(requestBody, settings);
            return Encoding.UTF8.GetBytes(body);
        }
    }
}
