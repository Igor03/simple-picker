using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventQueryResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FindEventQueryHandler : IRequestHandler<FindEventQuery, OneOf<Success, NotFound>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public FindEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<Success, NotFound>> Handle(FindEventQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var eventEntity = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (eventEntity is null)
            {
                return new NotFound("Either the event has been finished or it never existed");
            }

            var @event = new Success(_mapper.Map<EventDto>(eventEntity));

            return @event;
        }
    }
}
