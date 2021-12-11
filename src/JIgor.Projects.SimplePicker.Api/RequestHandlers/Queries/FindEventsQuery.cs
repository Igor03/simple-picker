using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventsQueryHandlerResponse;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventsQuery : IRequest<OneOf<Success, NotFound>>
    {
    }
}
