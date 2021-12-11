using System;
using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class PickValueCommand : IRequest<IEnumerable<EventValueDto>>
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
