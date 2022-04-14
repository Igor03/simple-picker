using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;
using OneOf;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.CreateEventCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, OneOf<Success, NoValuesAttached>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository, 
            ISimplePickerDatabaseContext simplePickerDatabaseContext, 
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
            _mapper = mapper;
        }

        public async Task<OneOf<Success, NoValuesAttached>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            // This condition will probably never be satisfied because we'll be validating the payload using FluentValidation
            if (!request.Event.EventValues.Any())
            {
                return new NoValuesAttached(
                    "You need to have, at least, one value attached in order to create an event.");
            }

            var @event = _mapper.Map<Event>(request.Event);

            await _eventRepository.CreateEventAsync(@event, cancellationToken)
                .ConfigureAwait(false);

            _ = await _simplePickerDatabaseContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Success(@event.Id);
        }
    }
}
