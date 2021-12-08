using System;
using JIgor.Projects.SimplePicker.Api.Dtos;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public CreateEventCommand(CreateEventRequestDto @event)
        {
            Event = @event;
        }

        public CreateEventRequestDto Event { get; set; }
    }
}
