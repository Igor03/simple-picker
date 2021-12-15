using System;
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
        public async Task FindEventsShouldReturnExpectedResult()
        {
            // Arrange
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

        [TestMethod]
        public async Task FindEventsShouldReturnNotFound()
        {
            // Arrange
            var outputMessage = "There aren't any active event at the moment!";
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FindEventsQuery>(p => p != null), CancellationToken.None)
                .Returns(new FindEventsQueryResponses.NotFound(outputMessage));

            var eventController = CreateEvenController(mediator);

            // Act
            var result = await eventController.FindEvents().ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which.Value
                .Should().BeEquivalentTo(outputMessage);
        }

        [TestMethod]
        public async Task FindEventShouldReturnExpectedResult()
        {
            // Arrange
            var output = FindEventsShouldReturnNotFoundOutput();
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FindEventQuery>(p => p != null && p.EventId != default), CancellationToken.None)
                .Returns(new FindEventQueryResponses.Success(output));

            var eventController = CreateEvenController(mediator);

            // Act
            var result = await eventController.FindEvent(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which.Value
                .Should().BeEquivalentTo(output);
        }

        [TestMethod]
        public async Task FindEventShouldReturnNotFound()
        {
            // Arrange
            var outputMessage = "Either the event has been finished or it never existed";
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FindEventQuery>(p => p != null && p.EventId != default), CancellationToken.None)
                .Returns(new FindEventQueryResponses.NotFound(outputMessage));

            var eventController = CreateEvenController(mediator);

            // Act
            var result = await eventController.FindEvent(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which.Value
                .Should().BeEquivalentTo(outputMessage);
        }

        private static EventController CreateEvenController(IMediator mediator = null)
        {
            // mediator =  mediator ?? Substitute.For<IMediator>();
            mediator ??= Substitute.For<IMediator>();

            return new EventController(mediator);
        }
    }
}
