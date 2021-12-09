﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Event> FindEventAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var @event = await _simplePickerDatabaseContext.Events.Where(p => p.Id == eventId && !p.IsFinished)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return @event;
        }

        public async Task<IEnumerable<Event>> FindEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _simplePickerDatabaseContext
                .Events.Include(p => p.EventValues)
                .Where(p => !p.IsFinished)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return events;
        }
    }
}
