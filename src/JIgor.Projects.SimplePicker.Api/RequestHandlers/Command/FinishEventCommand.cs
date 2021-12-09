using System;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FinishEventCommandResponse;

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
