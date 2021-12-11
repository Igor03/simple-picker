using System;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class CreateEventCommandResponses
    {
        public readonly struct Success
        {
            public Success(Guid eventId)
            {
                EventId = eventId;
            }

            public Guid EventId { get; }
        }
    }
}
