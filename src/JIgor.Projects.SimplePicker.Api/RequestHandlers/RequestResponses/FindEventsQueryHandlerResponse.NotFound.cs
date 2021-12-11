using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FindEventsQueryHandlerResponse
    {
        public readonly struct NotFound
        {
            public NotFound(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}
