using JIgor.Projects.SimplePicker.Api.Database.Contracts;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;
using OneOf;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FinishEventCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class FinishEventCommandHandler : IRequestHandler<FinishEventCommand, OneOf<Success, NotFound>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public FinishEventCommandHandler(IEventRepository eventRepository, ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        public async Task<OneOf<Success, NotFound>> Handle(FinishEventCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var @event = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (@event is null)
            {
                return new NotFound($"Unable to finish the event {request.EventId} this event!" +
                                    $" Maybe the event is already finished ot it never existed.");
            }

            @event.IsFinished = true;
            @event.EventValues.Where(e => !e.IsPicked)
                .ToList().ForEach(ev =>
                {
                    ev.IsPicked = true;
                });

            _ = await _simplePickerDatabaseContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return new Success(@event.Id);
        }
    }
}