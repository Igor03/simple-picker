using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JIgor.Projects.SimplePicker.Api.Data.Contracts;
using JIgor.Projects.SimplePicker.Api.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace JIgor.Projects.SimplePicker.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly ISimplePickerDatabaseContext _simplePickerDatabaseContext;

        public EventController(IEventRepository eventRepository, ISimplePickerDatabaseContext simplePickerDatabaseContext)
        {
            _eventRepository = eventRepository;
            _simplePickerDatabaseContext = simplePickerDatabaseContext;
        }

        // GET: api/<EventController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var @event = new Event()
            {
                Title = "My title",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                IsFinished = true,
            };

            await _eventRepository.CreateEventAsync(@event, CancellationToken.None).ConfigureAwait(false);
            _ = await _simplePickerDatabaseContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            return Ok(@event.Id);
        }
    }
}
