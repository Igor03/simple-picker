using System;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class FinishEventCommand : IRequest<Guid>
    {
        public FinishEventCommand(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}
