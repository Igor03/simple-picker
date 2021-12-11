using JIgor.Projects.SimplePicker.Api.Dtos;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FindEventQueryResponses
    {
        public readonly struct Success
        {
            public Success(EventDto @event)
            {
                Event = @event;
            }

            public EventDto Event { get; }
        }
    }
}
