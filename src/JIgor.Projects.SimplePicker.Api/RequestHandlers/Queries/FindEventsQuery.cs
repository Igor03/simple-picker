using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventsQueryResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventsQuery : IRequest<OneOf<Success, NotFound>>
    {
    }
}
