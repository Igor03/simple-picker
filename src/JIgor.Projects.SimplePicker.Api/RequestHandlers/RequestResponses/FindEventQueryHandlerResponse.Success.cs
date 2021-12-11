using JIgor.Projects.SimplePicker.Api.Dtos.Default;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FindEventQueryHandlerResponse
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
