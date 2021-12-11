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

            var listPicker = new ListPicker.ListPicker();
            var removedValues = listPicker
                .PickElements(@event.EventValues!.ToList(), request.NumberOfPicks);

            MarkAsPicked(@event.EventValues, removedValues.Select(p => p.EventId));

            await _simplePickerDatabaseContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<EventValueDto>>(removedValues);
        }

        private static void MarkAsPicked(IEnumerable<EventValue> eventValues, IEnumerable<Guid> pickedValuesIds)
        {
            var pickedValuesCounter = 0;
            var totalValuesToPick = pickedValuesIds.Count();

            foreach (var value in eventValues)
            {
                if (pickedValuesIds.Contains(value.EventId))
                {
                    value.IsPicked = true;
                    pickedValuesCounter += 1;
                }

                if (pickedValuesCounter == totalValuesToPick)
                {
                    return;
                }
            }
        }
    }
}
