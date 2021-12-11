using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class PickValueCommandResponses
    {
        public readonly struct Success
        {
            public Success(IEnumerable<EventValueDto> eventValues)
            {
                EventValues = eventValues;
            }

            public IEnumerable<EventValueDto> EventValues { get; }
        }
    }
}
