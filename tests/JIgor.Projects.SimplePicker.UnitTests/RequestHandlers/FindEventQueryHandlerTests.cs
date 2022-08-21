using AutoMapper;
using FluentAssertions;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.Entities;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using static JIgor.Projects.SimplePicker.UnitTests.RequestHandlers.RequestHandlersTestsDataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.RequestHandlers
{
    [TestClass]
    public class FindEventQueryHandlerTests
    {
        [TestMethod]
        public async Task FindEventQueryHandlerShouldReturnExpectedResult()
        {
            // Arrange
            var eventId = (Guid)GetFindEventQueryHandlerShouldReturnExpectedResultData()[0];
            var @event = (Event)GetFindEventQueryHandlerShouldReturnExpectedResultData()[1];
            var eventDto = (EventDto)GetFindEventQueryHandlerShouldReturnExpectedResultData()[2];

            var eventRepository = Substitute.For<IEventRepository>();
            var mapper = Substitute.For<IMapper>();

            eventRepository.FindEventAsync(eventId, CancellationToken.None)
                .Returns(@event);

            mapper.Map<EventDto>(Arg.Any<Event>())
                .Returns(eventDto);

            var queryHandler = FindEventQueryHandler(eventRepository, mapper);

            // Act
            var result = await queryHandler.Handle(new FindEventQuery(eventId), CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Value.Should().NotBeNull().And
                .BeOfType<FindEventQueryResponses.Success>()
                .Which.Event.Should().BeEquivalentTo(eventDto);
        }

        [TestMethod]
        public async Task FindEventQueryHandlerShouldReturnNotFound()
        {
            // Arrange
            const string outputMessage = "Either the event has been finished or it never existed";

            var eventRepository = Substitute.For<IEventRepository>();
            eventRepository.FindEventAsync(Arg.Is<Guid>(p => p != default), CancellationToken.None)
                .Returns(new Event());

            var queryHandler = FindEventQueryHandler(eventRepository);

            // Act
            var result = await queryHandler.Handle(new FindEventQuery(default!), CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Value.Should().NotBeNull()
                .And.BeOfType<FindEventQueryResponses.NotFound>()
                .Which.Message.Should().Be(outputMessage);
        }

        [TestMethod]
        public async Task FindEventQueryHandlerShouldThrowArgumentNullException()
        {
            // Arrange
            var queryHandler = FindEventQueryHandler();

            // Act
            Func<Task> result = async () => await
                queryHandler.Handle(default!, CancellationToken.None)
                    .ConfigureAwait(false);

            // Assert
            _ = await result.Should()
                .ThrowExactlyAsync<ArgumentNullException>(because: "Was provided a null value as a parameter.")
                .ConfigureAwait(false);
        }

        private static FindEventQueryHandler FindEventQueryHandler(
            IEventRepository eventRepository = null,
            IMapper mapper = null)
        {
            eventRepository ??= Substitute.For<IEventRepository>();
            mapper ??= Substitute.For<IMapper>();

            return new FindEventQueryHandler(eventRepository, mapper);
        }
    }
}