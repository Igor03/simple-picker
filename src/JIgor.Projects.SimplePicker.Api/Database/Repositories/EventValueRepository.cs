using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.Api.Database.Repositories
{
    public class EventValueRepository : IEventValueRepository
    {
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public EventValueRepository(ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        public async Task AttachValuesAsync(IEnumerable<EventValue> eventValues, CancellationToken cancellationToken)
        {
            _ = eventValues ?? throw new ArgumentNullException(nameof(eventValues));

            await _simplePickerDatabaseContext.EventValues
                .AddRangeAsync(eventValues, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}