using JIgor.Projects.SimplePicker.Api.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.Api.Database.Contracts
{
    public interface IEventValueRepository
    {
        Task AttachValuesAsync(IEnumerable<EventValue> eventValues, CancellationToken cancellationToken);
    }
}