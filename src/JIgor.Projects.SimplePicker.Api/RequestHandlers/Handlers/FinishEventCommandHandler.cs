using System;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FinishEventCommandHandler : IRequestHandler<FinishEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public FinishEventCommandHandler(IEventRepository eventRepository, ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        public async Task<Guid> Handle(FinishEventCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var @event = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (@event is null)
            {
                // OneOf treatment here
                throw new Exception("Unable to find this event!");
            }

            @event.IsFinished = true;

            _ = await _simplePickerDatabaseContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return @event.Id;
        }
    }
}
