using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
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

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var @event = _mapper.Map<Event>(request.Event);

            await _eventRepository.CreateEventAsync(@event, cancellationToken)
                .ConfigureAwait(false);

            _ = await _simplePickerDatabaseContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return @event.Id;
        }
    }
}
