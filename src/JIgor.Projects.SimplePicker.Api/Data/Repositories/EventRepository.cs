using System;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.Api.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public EventRepository(ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        public async Task CreateEventAsync(Event @event, CancellationToken cancellationToken)
        {
            _ = @event ?? throw new ArgumentNullException(nameof(@event));

            _ = await _simplePickerDatabaseContext.Events
                .AddAsync(@event, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
