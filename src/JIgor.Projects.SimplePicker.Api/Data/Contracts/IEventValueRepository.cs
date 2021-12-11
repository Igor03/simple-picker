using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.Api.Data.Contracts
{
    public interface IEventValueRepository
    {
        Task AttachValuesAsync(IEnumerable<EventValue> eventValues, CancellationToken cancellationToken);


        Task<IEnumerable<EventValue>> FindEventValues(Guid eventId, CancellationToken cancellationToken);
    }
}
