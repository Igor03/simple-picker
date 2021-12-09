using System;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventQueryHandlerResponse;


namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventQuery : IRequest<OneOf<Success, NotFound>>
    {
        public FindEventQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
