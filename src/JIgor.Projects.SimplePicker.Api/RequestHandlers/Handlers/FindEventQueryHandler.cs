using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FindEventQueryHandler : IRequestHandler<FindEventQuery, EventDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public FindEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(FindEventQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var eventEntity = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (eventEntity is null)
            {
                throw new Exception("Either the event has been finished or it never existed");
            }

            var @event = _mapper.Map<EventDto>(eventEntity);

            return @event;
        }
    }
}
