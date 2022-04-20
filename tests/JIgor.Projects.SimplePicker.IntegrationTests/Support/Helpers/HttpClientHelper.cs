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

        public async Task<object> CreateEventAsync(object body)
        {
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
            };

            var @event = new EventDto()
            {
                Description = "Description",
                DueDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now,
                Title = "Title",
                EventValues = new List<EventValueDto>()
                {
                    new EventValueDto("Value"),
                    new EventValueDto("Value")
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(@event), Encoding.UTF8, "application/json");
            var created = await _httpClient.PostAsync("api/v1/Event/Create", content);
            var response = await _httpClient.GetAsync("api/v1/Event");
            return response;
        }
    }
}
