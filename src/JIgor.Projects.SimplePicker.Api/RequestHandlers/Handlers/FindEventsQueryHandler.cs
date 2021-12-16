using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventsQueryResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FindEventsQueryHandler : IRequestHandler<FindEventsQuery, OneOf<Success, NotFound>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public FindEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<Success, NotFound>> Handle(FindEventsQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var eventEntities = await _eventRepository
                .FindEventsAsync(cancellationToken)
                .ConfigureAwait(false);

            if (eventEntities is null || !eventEntities.Any())
            {
                return new NotFound("There aren't any active events at the moment!");
            }

            var events = _mapper.Map<IEnumerable<EventDto>>(eventEntities);

            return new Success(events);
        }
    }
}
