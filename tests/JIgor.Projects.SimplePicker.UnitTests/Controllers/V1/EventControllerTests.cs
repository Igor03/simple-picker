﻿using FluentAssertions;
using JIgor.Projects.SimplePicker.Api.Controllers.V1;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

            var eventController = CreateEventController(mediator);

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

            var eventController = CreateEventController(mediator);

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

            var eventController = CreateEventController(mediator);

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

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.FindEvent(Guid.NewGuid()).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which.Value
                .Should().BeEquivalentTo(outputMessage);
        }

        [TestMethod]
        public async Task FindEventShouldReturnBadRequest()
        {
            // Arrange
            var eventController = CreateEventController();
            eventController.ModelState.AddModelError("error", "error");

            // Act
            var result = await eventController.FindEvent(default).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task CreateEventShouldReturnExpectedResult()
        {
            // Arrange
            var input = CreateEventShouldReturnExpectedResultInput();
            var output = Guid.NewGuid();
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<CreateEventCommand>(p => p != null && p.Event == input), CancellationToken.None)
                .Returns(new CreateEventCommandResponses.Success(output));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.CreateEvent(input).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which.Value
                .Should().BeEquivalentTo(output);
        }

        [TestMethod]
        public async Task CreateEventShouldReturnBadRequest()
        {
            // Arrange
            var input = CreateEventShouldReturnBadRequestInput();
            var outputMessage = "You need to have, at least, one value attached in order to create an event.";
            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<CreateEventCommand>(p => p != null && p.Event == input), CancellationToken.None)
                .Returns(new CreateEventCommandResponses.NoValuesAttached(outputMessage));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.CreateEvent(input).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<BadRequestObjectResult>().Which.Value
                .Should().BeEquivalentTo(outputMessage);
        }

        [TestMethod]
        public async Task CreateEventShouldReturnBadRequest_1()
        {
            // Arrange
            var eventController = CreateEventController();
            eventController.ModelState.AddModelError("error", "error");

            // Act
            var result = await eventController.CreateEvent(default!).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task AttachEventValueShouldReturnExpectedResult()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var inputValues = GetAttachEventValueShouldReturnExpectedResultInput();

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<AttachEventValueCommand>(p => p != null
                                                               && p.EventId == eventId
                                                               && Equals(p.EventValues, inputValues)),
                    CancellationToken.None)
                .Returns(new AttachEventValueCommandResponses.Success(eventId));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.AttachEventValue(eventId, inputValues)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which.Value
                .Should().BeEquivalentTo(eventId);
        }

        [TestMethod]
        public async Task AttachEventValueShouldReturnNotFound()
        {
            // Arrange
            var eventId = default(Guid);
            var outputMessage = $"Either the event {eventId} is already finished or it never existed!";

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Any<AttachEventValueCommand>())
                .Returns(new AttachEventValueCommandResponses.NotFound(outputMessage));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.AttachEventValue(eventId, default!)
                .ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which.Value
                .Should().BeEquivalentTo(outputMessage);
        }

        [TestMethod]
        public async Task AttachEventValueShouldReturnBadRequest()
        {
            // Arrange
            var eventController = CreateEventController();
            eventController.ModelState.AddModelError("error", "error");

            // Act
            var result = await eventController.AttachEventValue(default, default!).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task PickValueShouldReturnExpectedResult()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<PickValueCommand>(p => p.EventId == eventId && p.NumberOfPicks > 0),
                    CancellationToken.None)
                .Returns(new PickValueCommandResponses.Success(new List<EventValueDto>()));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.PickEventValue(eventId, 3).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which
                .Value.Should().BeOfType<List<EventValueDto>>();
        }

        [TestMethod]
        public async Task PickValueShouldReturnNotFoundOutput()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var outputMessage = "Either the event is already finished or it never existed!";

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<PickValueCommand>(p => p.EventId == eventId && p.NumberOfPicks > 0),
                    CancellationToken.None)
                .Returns(new PickValueCommandResponses.NotFound(outputMessage));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.PickEventValue(eventId, 3).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which
                .Value.Should().Be(outputMessage);
        }

        [TestMethod]
        public async Task PickValueShouldReturnBadRequest()
        {
            // Arrange
            var eventController = CreateEventController();
            eventController.ModelState.AddModelError("error", "error");

            // Act
            var result = await eventController.PickEventValue(default, default!).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task FinishEventShouldReturnExpectedResult()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FinishEventCommand>(p => p.EventId == eventId),
                    CancellationToken.None)
                .Returns(new FinishEventCommandResponses.Success(eventId));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.FinishEvent(eventId).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<OkObjectResult>().Which
                .Value.Should().Be(eventId);
        }

        [TestMethod]
        public async Task FinishEventShouldReturnNotFoundOutput()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var outputMessage = $"Unable to finish the event {eventId} this event!" +
                                $" Maybe the event is already finished ot it never existed.";

            var mediator = Substitute.For<IMediator>();
            mediator.Send(Arg.Is<FinishEventCommand>(p => p.EventId == eventId),
                    CancellationToken.None)
                .Returns(new FinishEventCommandResponses.NotFound(outputMessage));

            var eventController = CreateEventController(mediator);

            // Act
            var result = await eventController.FinishEvent(eventId).ConfigureAwait(false);

            // Assert
            _ = result.Should().NotBeNull().And
                .BeOfType<NotFoundObjectResult>().Which
                .Value.Should().Be(outputMessage);
        }

        private static EventController CreateEventController(IMediator mediator = null)
        {
            // mediator =  mediator ?? Substitute.For<IMediator>();
            mediator ??= Substitute.For<IMediator>();

            return new EventController(mediator);
        }
    }
}