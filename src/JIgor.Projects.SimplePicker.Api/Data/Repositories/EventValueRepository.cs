using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace JIgor.Projects.SimplePicker.Api.Data.Repositories
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

        public async Task<IEnumerable<EventValue>> FindEventValues(Guid eventId, CancellationToken cancellationToken)
        {
            var eventValues = await _simplePickerDatabaseContext
                .EventValues.Where(p => p.EventId == eventId)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return eventValues;
        }
    }
}