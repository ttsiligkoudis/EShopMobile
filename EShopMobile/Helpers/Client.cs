using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EShopMobile.Helpers
{
    public class Client<T>
    {
        private readonly HttpClient _client;
        private string baseUrl = DeviceInfo.Current.Platform == DevicePlatform.Android ? "https://9d7d-85-72-60-202.eu.ngrok.io/api/" : "https://localhost:44384/api/";

        public Client()
        {
            var handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            _client = new HttpClient(handler);
        }

        public async Task<T> GetAsync(string api)
        {
            using (var response = await _client.GetAsync(baseUrl + api))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apiResponse) && response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(apiResponse);
            }
            return default;
        }

        public async Task<IEnumerable<T>> GetListAsync(string api)
        {
            using (var response = await _client.GetAsync(baseUrl + api))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apiResponse) && response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
            }
            return default;
        }

        public async Task<T> PutAsync(T data, string api)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _client.PutAsync(baseUrl + api, content))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apiResponse) && response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(apiResponse);
            }
            return default;
        }

        public async Task<T> PostAsync(T data, string api)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _client.PostAsync(baseUrl + api, content))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apiResponse) && response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(apiResponse);
            }
            return default;
        }

        public async Task<IEnumerable<T>> PostListAsync(IEnumerable<T> data, string api)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _client.PostAsync(baseUrl + api, content))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(apiResponse) && response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
            }
            return default;
        }

        public async Task<string> DeleteAsync(int id, string api)
        {
            using var response = await _client.DeleteAsync(baseUrl + api);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return apiResponse;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
