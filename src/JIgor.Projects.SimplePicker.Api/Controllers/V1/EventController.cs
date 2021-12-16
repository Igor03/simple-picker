using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Queries;
using MediatR;

namespace JIgor.Projects.SimplePicker.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FindEvents()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new FindEventsQuery(), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.Events),
                notFound => NotFound(notFound.Message));
        }

        [HttpGet("{eventId:guid}")]
        public async Task<IActionResult> FindEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new FindEventQuery(eventId), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.Event),
                notFound => NotFound(notFound.Message));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new CreateEventCommand(@event), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventId),
                noValuesAttached => BadRequest(noValuesAttached.Message));
        }

        [HttpPost("Attach/{eventId:guid}")]
        public async Task<IActionResult> CreateEventValue(Guid eventId, [FromBody] IEnumerable<EventValueDto> eventValues)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new AttachEventValueCommand(eventId, eventValues), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventId),
                notFound => NotFound(notFound.Message));
        }

        [HttpPost("Pick/{eventId:guid}")]
        public async Task<IActionResult> CreateEventValue(Guid eventId, [FromQuery] int numberOfPicks = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new PickValueCommand(eventId, numberOfPicks), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventValues),
                notFound => NotFound(notFound.Message));
        }

        [HttpDelete("Finish")]
        public async Task<IActionResult> FinishEvent([FromQuery] Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mediator
                .Send(new FinishEventCommand(eventId), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventId),
                notFound => NotFound(notFound.Message));
        }
    }
}
