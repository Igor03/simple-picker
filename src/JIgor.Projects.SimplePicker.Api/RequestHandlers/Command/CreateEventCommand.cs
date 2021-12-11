using System;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.CreateEventCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class CreateEventCommand : IRequest<OneOf<Success, NoValuesAttached>>
    {
        public CreateEventCommand(EventDto @event)
        {
            Event = @event;
        }

        public EventDto Event { get; set; }
    }
}
