﻿using MediatR;
using OneOf;
using System;
using static JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses.FindEventQueryResponses;

namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries
{
    public class FindEventQuery : IRequest<OneOf<Success, NotFound>>
    {
        public FindEventQuery(Guid eventId)
        {
            EventId = eventId;
        }

        public Guid EventId { get; set; }
    }
}