using System;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public CreateEventCommand(EventDto @event)
        {
            Event = @event;
        }

        public EventDto Event { get; set; }
    }
}
