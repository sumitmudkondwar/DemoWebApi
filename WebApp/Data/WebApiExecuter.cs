using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApp.Authority;

namespace WebApp.Data
{
    public class WebApiExecuter(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";
        private const string AuthApiName = "AuthorityApi";

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        { 
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);
            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
            await HandlePotentialError(response);
        }

        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJwtToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);
            await HandlePotentialError(response);
        }

        private async Task HandlePotentialError(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var errorJson = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }
        }

        private async Task AddJwtToHeader(HttpClient httpClient)
        {
            var clientId = configuration.GetValue<string>("ClientId");
            var secret = configuration.GetValue<string>("Secret");

            var authoClient = httpClientFactory.CreateClient(AuthApiName);
            var response = await authoClient.PostAsJsonAsync("auth", new AppCredential
            {
                ClientId = clientId,
                Secret = secret
            });

            response.EnsureSuccessStatusCode();

            string strToken = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strToken);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);


        }
    }
}
