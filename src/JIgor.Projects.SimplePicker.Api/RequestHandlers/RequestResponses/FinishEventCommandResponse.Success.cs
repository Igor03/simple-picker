using System;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FinishEventCommandResponse
    {
        public struct Success
        {
            public Success(Guid eventId)
            {
                EventId = eventId;
            }

            public Guid EventId { get; set; }
        }
    }
}
