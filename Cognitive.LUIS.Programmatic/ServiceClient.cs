using Cognitive.LUIS.Programmatic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.Retry;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive.LUIS.Programmatic
{
    public class ServiceClient : IDisposable
    {
        private static readonly MediaTypeHeaderValue CONTENT_TYPE = new MediaTypeHeaderValue("application/json");

        private readonly HttpClient _client;
        private readonly AsyncRetryPolicy _policy;

        public ServiceClient(string subscriptionKey, Regions region, RetryPolicyConfiguration retryPolicyConfiguration)
        {
            var baseUrl = $"https://{region.ToString().ToLower()}.api.cognitive.microsoft.com/luis/api/v2.0/";
            _client = HttpClientFactory.Create(baseUrl, subscriptionKey);
            _policy = GetPolicy(retryPolicyConfiguration ?? RetryPolicyConfiguration.Default);
        }

        protected async Task<string> Get(string path)
        {
            return await _policy.ExecuteAsync(async () =>
            {
                var response = await _client.GetAsync(path);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return responseContent;
                else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    var serviceException = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                    throw serviceException.ToException();
                }
                return null;
            });
        }

        protected async Task<string> Post(string path)
        {
            return await _policy.ExecuteAsync(async () =>
            {
                var response = await _client.PostAsync(path, null);
                return await GetResponseContent(response);
            });
        }

        protected async Task<string> Post<TRequest>(string path, TRequest requestBody)
        {
            return await _policy.ExecuteAsync(async () =>
            {
                using (var content = new ByteArrayContent(GetByteData(requestBody)))
                {
                    content.Headers.ContentType = CONTENT_TYPE;
                    var response = await _client.PostAsync(path, content);
                    return await GetResponseContent(response);
                }
            });
        }

        protected async Task Put<TRequest>(string path, TRequest requestBody)
        {
            await _policy.ExecuteAsync(async () =>
            {
                using (var content = new ByteArrayContent(GetByteData(requestBody)))
                {
                    content.Headers.ContentType = CONTENT_TYPE;
                    var response = await _client.PutAsync(path, content);
                    await GetResponseContent(response);
                }
            });
        }

        protected async Task Delete(string path)
        {
            await _policy.ExecuteAsync(async () =>
            {
                var response = await _client.DeleteAsync(path);
                await GetResponseContent(response);
            });
        }

        private async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var serviceException = JsonConvert.DeserializeObject<ServiceException>(responseContent);
                throw serviceException.ToException();
            }
            return responseContent;
        }

        private byte[] GetByteData<TRequest>(TRequest requestBody)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var body = JsonConvert.SerializeObject(requestBody, settings);
            return Encoding.UTF8.GetBytes(body);
        }

        private AsyncRetryPolicy GetPolicy(RetryPolicyConfiguration retryPolicyConfiguration)
        {
            return Policy
            .Handle<Exception>(e =>
            {
                var errorCode = e.Message.Split('-')
                                         .Select(p => p.Trim())
                                         .FirstOrDefault();

                if (string.IsNullOrEmpty(errorCode))
                    return false;

                if (errorCode == Error.UNEXPECTED_ERROR_CODE)
                    return false;

                return Error.ERROR_LIST.Contains(errorCode);
            })
            .WaitAndRetryAsync(retryPolicyConfiguration.RetryCount, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryPolicyConfiguration.RetryAttemptFactor))
            );
        }

        public void Dispose() =>
            _client.Dispose();
    }
}
