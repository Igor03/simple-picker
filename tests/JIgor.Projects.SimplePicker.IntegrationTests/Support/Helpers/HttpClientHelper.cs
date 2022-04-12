using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
