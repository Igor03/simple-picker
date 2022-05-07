using FluentAssertions;
using JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace JIgor.Projects.SimplePicker.IntegrationTests.StepDefinitions
{
    [Binding]
    internal class HealthcheckSteps
    {
        private HttpClientHelper _httpClient;
        private int _result = 340;
        private string _baseUrl;

        public HealthcheckSteps(HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        [Given(@"the base url is (.*)")]
        public void Given(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        [When(@"send a Http Get request")]
        public async Task SendHttpGetRequest()
        {
            var httpResponse = await this._httpClient
                .CheckApplicationHealth();

            this._result = (int)httpResponse.StatusCode;
        }

        [Then(@"the http status code should be (.*)")]
        public void TheStatusCodeShouldBe(int statusCode)
        {
            this._result.Should().Be(statusCode);
        }
    }
}