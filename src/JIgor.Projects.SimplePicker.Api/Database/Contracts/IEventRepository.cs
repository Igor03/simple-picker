using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.Api.Database.Contracts
{
    public interface IEventRepository
    {
        Task CreateEventAsync(Event @event, CancellationToken cancellationToken);

        Task<Event> FindEventAsync(Guid eventId, CancellationToken cancellationToken);
        
        Task<IEnumerable<Event>> FindEventsAsync(CancellationToken cancellationToken);
    }
}
