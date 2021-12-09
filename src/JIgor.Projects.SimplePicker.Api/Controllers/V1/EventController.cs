using System;
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
            var result = await _mediator
                .Send(new FindEventsQuery(), CancellationToken.None)
                .ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> FindEvent(Guid eventId)
        {
            var result = await _mediator
                .Send(new FindEventQuery(eventId), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.Event),
                notFound => NotFound(notFound.Message));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequestDto @event)
        {
            _ = @event ?? throw new ArgumentNullException(nameof(@event));

            var result = await _mediator
                .Send(new CreateEventCommand(@event), CancellationToken.None)
                .ConfigureAwait(false);

            return Ok(result);
        }

        [HttpDelete("Finish")]
        public async Task<IActionResult> FinishEvent([FromQuery] Guid eventId)
        {
            var result = await _mediator
                .Send(new FinishEventCommand(eventId), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventId),
                notFound => NotFound(notFound.Message));
        }
    }
}
