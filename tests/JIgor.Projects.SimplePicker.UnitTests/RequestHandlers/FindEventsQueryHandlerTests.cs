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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static JIgor.Projects.SimplePicker.UnitTests.RequestHandlers.RequestHandlersTestsDataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.RequestHandlers
{
    [TestClass]
    public class FindEventsQueryHandlerTests
    {
        [TestMethod]
        public async Task FindEventsQueryHandlerShouldReturnExpectedResult()
        {
            // Arrange
            var eventEntities = (List<Event>)GetFindEventsQueryHandlerShouldReturnExpectedResultData()[0];
            var mappedEventEntities = (List<EventDto>)GetFindEventsQueryHandlerShouldReturnExpectedResultData()[1];

            var eventRepository = Substitute.For<IEventRepository>();
            var mapper = Substitute.For<IMapper>();

            eventRepository.FindEventsAsync(CancellationToken.None)
                .Returns(eventEntities);

            mapper.Map<IEnumerable<EventDto>>(Arg.Any<List<Event>>())
                .Returns(mappedEventEntities);

            var queryHandler = FindEventsQueryHandler(eventRepository, mapper);

            // Act
            var result = await queryHandler.Handle(new FindEventsQuery(), CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Value.Should().NotBeNull().And
                .BeOfType<FindEventsQueryResponses.Success>()
                .Which.Events.Should().BeEquivalentTo(mappedEventEntities);
        }

        [TestMethod]
        public async Task FindEventsQueryHandlerShouldReturnNotFound()
        {
            // Arrange
            const string outputMessage = "There aren't any active events at the moment!";

            var eventRepository = Substitute.For<IEventRepository>();
            eventRepository.FindEventsAsync(CancellationToken.None)
                .Returns(new List<Event>());

            var queryHandler = FindEventsQueryHandler(eventRepository);

            // Act
            var result = await queryHandler.Handle(new FindEventsQuery(), CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = result.Value.Should().NotBeNull()
                .And.BeOfType<FindEventsQueryResponses.NotFound>()
                .Which.Message.Should().Be(outputMessage);
        }

        [TestMethod]
        public async Task FindEventsQueryHandlerShouldThrowArgumentNullException()
        {
            // Arrange
            var queryHandler = FindEventsQueryHandler();

            // Act
            Func<Task> result = async () => await
                queryHandler.Handle(default!, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = await result.Should()
                .ThrowExactlyAsync<ArgumentNullException>(because: "Was provided a null value as a parameter.")
                .ConfigureAwait(false);
        }

        private static FindEventsQueryHandler FindEventsQueryHandler(
            IEventRepository eventRepository = null,
            IMapper mapper = null)
        {
            eventRepository ??= Substitute.For<IEventRepository>();
            mapper ??= Substitute.For<IMapper>();

            return new FindEventsQueryHandler(eventRepository, mapper);
        }
    }
}