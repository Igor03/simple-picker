using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FindEventsQueryHandler : IRequestHandler<FindEventsQuery, IEnumerable<EventDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public FindEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> Handle(FindEventsQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var eventEntities = await _eventRepository
                .FindEventsAsync(cancellationToken)
                .ConfigureAwait(false);

            if (eventEntities is null)
            {
                // OneOf treatment here
                throw new Exception("There aren't any active event at the moment!");
            }

            var events = _mapper.Map<IEnumerable<EventDto>>(eventEntities);

            return events;
        }
    }
}
