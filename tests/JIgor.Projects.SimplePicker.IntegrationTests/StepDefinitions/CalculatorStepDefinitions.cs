using FluentAssertions;
using JIgor.Projects.SimplePicker.IntegrationTests.Support.Helpers;
using TechTalk.SpecFlow;

namespace JIgor.Projects.SimplePicker.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private int n1;
        private int n2;
        private int result;

        public CalculatorStepDefinitions()
        {
        }



        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.specflow.org/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            n1 = number;
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic

            n2 = number;
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic
            result = n2 + n1;

        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            this.result.Should().Be(result);
        }
    }
}