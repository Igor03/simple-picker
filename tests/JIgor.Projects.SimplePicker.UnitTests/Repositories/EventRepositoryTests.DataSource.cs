using System;
using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.UnitTests.Repositories
{
    public partial class EventRepositoryTests
    {
        internal static class DataSource
        {
            public static Event GetCreateEventAsyncShouldReturnExpectedResultInputData()
            {
                var eventId = Guid.NewGuid();

                var seed = new Event()
                {
                    Id = eventId,
                    Title = "Some title",
                    Description = "Some description",
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(1),
                    IsFinished = false,
                    EventValues = new List<EventValue>()
                    {
                        new EventValue(Guid.NewGuid(), eventId, "Breeze", false),
                        new EventValue()
                        {
                            Id = Guid.NewGuid(),
                            EventId = eventId,
                            Value = "Brady",
                            IsPicked = false
                        }
                    }
                };

                return seed;
            }
        }
    }
}
