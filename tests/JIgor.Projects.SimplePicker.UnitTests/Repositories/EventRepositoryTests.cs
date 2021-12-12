using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JIgor.Projects.SimplePicker.Api.Database.DataContexts;
using JIgor.Projects.SimplePicker.Api.Database.Repositories;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using static JIgor.Projects.SimplePicker.UnitTests.Repositories.EventRepositoryTests.DataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.Repositories
{
    [TestClass]
    public partial class EventRepositoryTests
    {
        [TestMethod]
        public async Task CreateEventAsyncShouldReturnExpectedResult()
        {
            // Arrange
            var inputData = GetCreateEventAsyncShouldReturnExpectedResultInputData();
            var eventId = inputData.Id;

            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventRepository(simplePickerDatabaseContext);

            // Act
            await repository.CreateEventAsync(inputData, CancellationToken.None).ConfigureAwait(false);
            _ = await simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            // Assert
            using var assertionScope = new AssertionScope();
            _ = simplePickerDatabaseContext.Events.ToList()
                .Should().NotBeEmpty();
            _ = simplePickerDatabaseContext.Events.ToList().Find(p => p.Id == eventId)
                .Should().NotBeNull();
        }

        [TestMethod]
        public async Task CreateEventAsyncShouldThrowArgumentNullException()
        {
            // Arrange
            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventRepository(simplePickerDatabaseContext);

            // Act
            Func<Task> action = async () => await repository
                .CreateEventAsync(default!, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = action.Should()
                .ThrowExactlyAsync<ArgumentNullException>(because: "Was provided a null value as a parameter.")
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task FindEventsAsyncShouldReturnExpectedResult()
        {
            // Arrange
            var inputData = FindEventsAsyncShouldReturnExpectedResultInputData().ToList();
            var eventId1 = inputData[0].Id;
            var eventId2 = inputData[1].Id;

            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventRepository(simplePickerDatabaseContext);

            await simplePickerDatabaseContext.AddRangeAsync(inputData, CancellationToken.None)
                .ConfigureAwait(false);
            _ = await simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            // Act
            var result = await repository.FindEventsAsync(CancellationToken.None).ConfigureAwait(false);

            // Assert
            using var assertionScope = new AssertionScope();
            _ = result.Should().NotBeNullOrEmpty().And.HaveCountGreaterThan(0);
            _ = result.FirstOrDefault(p => p.Id == eventId1).Should().BeEquivalentTo(inputData[0]);
            _ = result.FirstOrDefault(p => p.Id == eventId2).Should().BeNull();
        }

        [TestMethod]
        public async Task FindEventAsyncShouldReturnExpectedResult()
        {
            // Arrange
            var inputData = FindEventAsyncShouldReturnExpectedResultInputData().ToList();
            var eventId1 = inputData[0].Id;
            var eventId2 = inputData[1].Id;

            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventRepository(simplePickerDatabaseContext);

            await simplePickerDatabaseContext.AddRangeAsync(inputData, CancellationToken.None)
                .ConfigureAwait(false);
            _ = await simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            // Act
            var result1 = await repository.FindEventAsync(eventId1, CancellationToken.None).ConfigureAwait(false);
            var result2 = await repository.FindEventAsync(eventId2, CancellationToken.None).ConfigureAwait(false);

            // Assert
            using var assertionScope = new AssertionScope();
            _ = result1.Should().NotBeNull().And.BeOfType<Event>().And.BeEquivalentTo(inputData[0]);
            // _ = result.Should().NotBeNull().And.BeOfType<Event>().Which.Id == eventId1;
            _ = result2.Should().BeNull();
        }

        [TestMethod]
        public async Task FindEventAsyncShouldThrowArgumentNullException()
        {
            // Arrange
            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventRepository(simplePickerDatabaseContext);

            // Act
            Func<Task> action = async () => await repository
                .FindEventAsync(default!, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _ = action.Should()
                .ThrowExactlyAsync<ArgumentNullException>(because: "Was provided a null value as a parameter.")
                .ConfigureAwait(false);
        }

        private static SimplePickerDatabaseContext CreateSimplePickerDatabaseContext(
            IConfiguration configuration = default)
        {
            configuration ??= Substitute.For<IConfiguration>();
            
            var options = new DbContextOptionsBuilder<SimplePickerDatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            return new SimplePickerDatabaseContext(options, configuration);
        }
    }
}
