using MediatR;
using OneOf;
using System;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.PickValueCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class PickValueCommand : IRequest<OneOf<Success, NotFound>>
    {
        public PickValueCommand(Guid eventId, int numberOfPicks)
        {
            EventId = eventId;
            NumberOfPicks = numberOfPicks;
        }

        public Guid EventId { get; set; }

        public int NumberOfPicks { get; set; }
    }
}