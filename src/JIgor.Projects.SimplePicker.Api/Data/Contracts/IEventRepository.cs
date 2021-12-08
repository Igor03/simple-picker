using System;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.Api.Data.Contracts
{
    public interface IEventRepository
    {
        Task CreateEventAsync(Event @event, CancellationToken cancellationToken);

        Task<Event> FindEventAsync(Guid eventId, CancellationToken cancellationToken);
    }
}
