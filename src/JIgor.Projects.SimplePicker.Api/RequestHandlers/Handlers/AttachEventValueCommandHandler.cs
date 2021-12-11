using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.AttachEventValueCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class AttachEventValueCommandHandler : IRequestHandler<AttachEventValueCommand, OneOf<Success, NotFound>>
    {
        private readonly IEventValueRepository _eventValueRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;
        private readonly IMapper _mapper;

        public AttachEventValueCommandHandler(IEventValueRepository eventValueRepository, 
            IEventRepository eventRepository, 
            ISimplePickerDatabaseContext simplePickerDatabaseContext,
            IMapper mapper)
        {
            _eventValueRepository = eventValueRepository;
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
            _mapper = mapper;
        }

        public async Task<OneOf<Success, NotFound>> Handle(AttachEventValueCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var @event = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (@event is null)
            {
                return new NotFound($"Either the event {request.EventId} is already finished or it never existed!");
            }

            var eventValueEntities = _mapper.Map<IEnumerable<EventValue>>(request.EventValues);
            eventValueEntities.ToList().ForEach(p => p.EventId = request.EventId);

            await _eventValueRepository
                .AttachValuesAsync(eventValueEntities, cancellationToken)
                .ConfigureAwait(false);

            _ = await _simplePickerDatabaseContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Success(@event.Id);
        }
    }
}
