using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FindEventsQueryResponses
    {
        public struct Success
        {
            public Success(IEnumerable<EventDto> events)
            {
                Events = events;
            }

            public IEnumerable<EventDto> Events { get; }
        }
    }
}
