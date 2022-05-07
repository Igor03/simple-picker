using MediatR;
using OneOf;
using System;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FinishEventCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class FinishEventCommand : IRequest<OneOf<Success, NotFound>>
    {
        public FinishEventCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}