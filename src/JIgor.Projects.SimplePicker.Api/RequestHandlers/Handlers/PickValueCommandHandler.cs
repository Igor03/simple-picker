using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using JIgor.Projects.SimplePicker.Api.Entities;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;
using JIgor.Projects.SimplePicker.Engine;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Handlers
{
    public class PickValueCommandHandler : IRequestHandler<PickValueCommand, IEnumerable<EventValueDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEventValueRepository _eventValueRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public PickValueCommandHandler(IMapper mapper, 
            IEventValueRepository eventValueRepository, 
            IEventRepository eventRepository, 
            ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _mapper = mapper;
            _eventValueRepository = eventValueRepository;
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        public async Task<IEnumerable<EventValueDto>> Handle(PickValueCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var @event = await _eventRepository
                .FindEventAsync(request.EventId, cancellationToken)
                .ConfigureAwait(false);

            if (@event is null)
            {
                throw new Exception("Either the event is already finished or it never existed!");
            }

            var removedValues = ListPicker
                .PickElements(@event.EventValues!.ToList(), request.NumberOfPicks, p => !p.IsPicked);

            // If there isn't any unpicked values
            // TODO: maybe move this to a database trigger
            if (!MarkAsPicked(@event.EventValues!, removedValues.Select(p => p.EventId)))
            {
                @event.IsFinished = true;
            }

            await _simplePickerDatabaseContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<EventValueDto>>(removedValues);
        }

        private static bool MarkAsPicked(IEnumerable<EventValue> eventValues, IEnumerable<Guid> pickedValuesIds)
        {
            var pickedValuesCounter = 0;
            var totalValuesToPick = pickedValuesIds.Count();

            // Checking if there is any pickable value
            if (!pickedValuesIds.Any())
            {
                return false;
            }

            foreach (var value in eventValues)
            {
                if (pickedValuesIds.Contains(value.EventId) && !value.IsPicked)
                {
                    value.IsPicked = true;
                    pickedValuesCounter += 1;
                }

                if (pickedValuesCounter == totalValuesToPick)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
