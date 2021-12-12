using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JIgor.Projects.SimplePicker.Api.Database.DataContexts;
using JIgor.Projects.SimplePicker.Api.Database.Repositories;
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
                .ThrowExactlyAsync<ArgumentNullException>(because: "Was provided and null value as a parameter.")
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
