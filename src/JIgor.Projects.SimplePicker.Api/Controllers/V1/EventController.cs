using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.RequestHandlers.Command;
using MediatR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
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

            return Ok(result);
        }
    }
}
