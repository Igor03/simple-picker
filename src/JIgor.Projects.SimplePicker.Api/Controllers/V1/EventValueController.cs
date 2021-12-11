using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Dtos.Default;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JIgor.Projects.SimplePicker.Api.Controllers.V1
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EventValueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventValueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{eventId}")]
        public async Task<IActionResult> CreateEventValue(Guid eventId, [FromBody] IEnumerable<EventValueDto> eventValues)
        {
            _ = eventValues ?? throw new ArgumentNullException(nameof(eventValues));

            var result = await _mediator
                .Send(new AttachEventValueCommand(eventId, eventValues), CancellationToken.None)
                .ConfigureAwait(false);

            return result.Match<IActionResult>(
                success => Ok(success.EventId),
                notFound => NotFound(notFound.Message));
        }


        [HttpPost("PickValue/{eventId}")]
        public async Task<IActionResult> CreateEventValue(Guid eventId, [FromQuery] int numberOfPicks = 1)
        {

            var result = await _mediator
                .Send(new PickValueCommand(eventId, numberOfPicks), CancellationToken.None)
                .ConfigureAwait(false);

            return Ok(result);
        }
    }
}
