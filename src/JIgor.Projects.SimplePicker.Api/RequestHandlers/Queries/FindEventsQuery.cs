using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventsQuery : IRequest<IEnumerable<EventDto>>
    {
    }
}
