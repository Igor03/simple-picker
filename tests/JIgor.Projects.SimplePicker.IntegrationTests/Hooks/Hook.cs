using JIgor.Projects.SimplePicker.Api.Database.DataContexts;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace JIgor.Projects.SimplePicker.IntegrationTests.Hooks
{
    [Binding]
    public sealed class Hook
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private SimplePickerDatabaseContext _dbContext;

        public Hook(SimplePickerDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BeforeScenario("@mytag", Order = 1)]
        public void BeforeScenarioWithTag()
        {
            var x = this._dbContext.Events.ToList();

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