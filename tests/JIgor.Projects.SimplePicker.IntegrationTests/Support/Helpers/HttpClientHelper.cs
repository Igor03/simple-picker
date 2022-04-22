using JIgor.Projects.SimplePicker.Api.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public HttpClientHelper(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Guid> CreateEventAsync(EventDto body)
        {
            var uri = "api/v1/Event/Create";

            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8, "application/json");

            var created = await _httpClient.PostAsync(uri, content);
            
            if (!created.IsSuccessStatusCode) return default;
            return JsonConvert.DeserializeObject<Guid>(
                await created.Content.ReadAsStringAsync());
        }

        public async Task<EventDto?> GetEvent(Guid eventId)
        {
            var uri = $"api/v1/Event/{eventId}";

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return null;
            return JsonConvert.DeserializeObject<EventDto?>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetEventAsync(Guid eventId)
        {
            var uri = $"api/v1/Event/{eventId}";

            var response = await _httpClient.GetAsync(uri);

            return response;
        }

        public async Task<HttpResponseMessage> CheckApplicationHealth()
        {
            var uri = $"CheckHealth";
            
            var response = await _httpClient.GetAsync(uri);

            return response;
        }

        public async Task<Guid> DeleteEvent(Guid eventId)
        {
            var uri = $"api/v1/Event/Finish?eventId={eventId}";
            
            var response = await _httpClient.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode) return Guid.Empty;
            return JsonConvert.DeserializeObject<Guid>(
                await response.Content.ReadAsStringAsync());

        }
    }
}
