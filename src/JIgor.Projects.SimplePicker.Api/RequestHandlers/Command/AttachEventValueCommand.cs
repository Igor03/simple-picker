﻿using JIgor.Projects.SimplePicker.Api.Dtos;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.AttachEventValueCommandResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Command
{
    public class AttachEventValueCommand : IRequest<OneOf<Success, NotFound>>
    {
        public AttachEventValueCommand(Guid eventId, IEnumerable<EventValueDto> eventValues)
        {
            EventId = eventId;
            EventValues = eventValues;
        }

        public Guid EventId { get; set; }

        public IEnumerable<EventValueDto> EventValues { get; set; }
    }
}