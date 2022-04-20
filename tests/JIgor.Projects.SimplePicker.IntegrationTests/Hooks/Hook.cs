using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Hooks
{
    [Binding]
    public sealed class Hook
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private DatabaseHelper _databaseClient;
        private HttpClientHelper _httpClient;

        public Hook(DatabaseHelper databaseClient, HttpClientHelper httpClient)
        {
            _databaseClient = databaseClient;
            _httpClient = httpClient;
        }

        [BeforeScenario("@mytag", Order = 1)]
        public async Task BeforeScenarioWithTag()
        {
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

            var returnedId = await _httpClient.CreateEventAsync(@event);
            await _httpClient.DeleteEvent(returnedId);
        }

        [BeforeScenario("mytag", Order = 2)]
        public void FirstBeforeScenario()
        {
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
            Console.WriteLine("ofijeiojf");
        }

        [AfterScenario]
        public void AfterScenario()
        {

            //TODO: implement logic that has to run after executing each scenario
        }
    }
}