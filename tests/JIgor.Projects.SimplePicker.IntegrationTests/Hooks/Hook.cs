using JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers;
using System;
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
            await _databaseClient.GetResource(new { igor = 2 })
                .ConfigureAwait(default);

            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario
            Console.WriteLine("ofijeiojf");
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