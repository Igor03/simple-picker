using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JIgor.Projects.SimplePicker.Api.Controllers.V1;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using static JIgor.Projects.SimplePicker.UnitTests.Controllers.V1.EventControllerTests.DataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.Controllers.V1
{
    [TestClass]
    public partial class EventControllerTests
    {
        [TestMethod]
        public async Task FindEventsShouldReturnsExpectedResult()
        {
            // Assert
            var output = FindEventsShouldReturnsExpectedResultOutput();
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FindEventsQuery>(p => p != null), CancellationToken.None)
                .Returns(new FindEventsQueryResponses.Success(output));

            var eventController = CreateEvenController(mediator);

            // Act
            var result = await eventController.FindEvents().ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which.Value
                .Should().BeEquivalentTo(output);
        }

        private static EventController CreateEvenController(IMediator mediator = null)
        {
            // mediator =  mediator ?? Substitute.For<IMediator>();
            mediator ??= Substitute.For<IMediator>();

            return new EventController(mediator);
        }
    }
}
