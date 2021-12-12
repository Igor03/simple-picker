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
using static JIgor.Projects.SimplePicker.UnitTests.Repositories.EventValueRepositoryTests.DataSource;

namespace JIgor.Projects.SimplePicker.UnitTests.Repositories
{
    [TestClass]
    public partial class EventValueRepositoryTests
    {
        [TestMethod]
        public async Task AttachValuesAsyncShouldReturnExpectedResult()
        {
            // Arrange
            var initialEvents = GetAttachValuesAsyncShouldReturnExpectedResultInputData().ToList();
            var eventId = initialEvents[0].Id;
            
            var eventValuesToBeAttached = new List<EventValue>()
            {
                new EventValue(Guid.NewGuid(), eventId, "Montana", false),
                new EventValue(Guid.NewGuid(), eventId, "Breeze", false),
                new EventValue(Guid.NewGuid(), eventId, "Favre", true)
            };

            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            await simplePickerDatabaseContext.Events.AddRangeAsync(initialEvents, CancellationToken.None)
                .ConfigureAwait(false);
            _ = simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
            
            var repository = new EventValueRepository(simplePickerDatabaseContext);

            // Act
            await repository.AttachValuesAsync(eventValuesToBeAttached, CancellationToken.None).ConfigureAwait(false);
            _ = await simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            // Assert
            simplePickerDatabaseContext.EventValues
                .Where(p => p.EventId == eventId)
                .Should().NotBeNullOrEmpty().And.HaveCount(5);
        }

        [TestMethod]
        public async Task AttachValuesAsyncShouldThrowArgumentNullException()
        {
            // Arrange
            await using var simplePickerDatabaseContext = CreateSimplePickerDatabaseContext();
            var repository = new EventValueRepository(simplePickerDatabaseContext);

            // Act
            Func<Task> action = async () => await repository
                .AttachValuesAsync(default!, CancellationToken.None)
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
