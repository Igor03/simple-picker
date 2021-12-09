using System;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventQuery : IRequest<EventDto>
    {
        public FindEventQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
