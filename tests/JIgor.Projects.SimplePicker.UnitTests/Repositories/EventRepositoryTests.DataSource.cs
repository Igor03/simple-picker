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

            public static IEnumerable<Event> FindEventsAsyncShouldReturnExpectedResultInputData()
            {
                var eventId1 = Guid.NewGuid();
                var eventId2 = Guid.NewGuid();

                var seed = new List<Event>()
                {
                    new Event(
                        eventId1,
                        "Some title",
                        "Some description",
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        false,
                        new List<EventValue>()
                        {
                            new EventValue(Guid.NewGuid(), eventId1, "Breeze", false),
                            new EventValue(Guid.NewGuid(), eventId1, "Brady", false),
                        }),
                    new Event()
                    {
                        Id = eventId2,
                        Title = "Some title",
                        Description = "Some description",
                        StartDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(1),
                        IsFinished = true,
                        EventValues = new List<EventValue>()
                        {
                            new EventValue(Guid.NewGuid(), eventId2, "Montana", true),
                        }
                    }
                };

                return seed;
            }

            public static IEnumerable<Event> FindEventAsyncShouldReturnExpectedResultInputData()
            {
                var eventId1 = Guid.NewGuid();
                var eventId2 = Guid.NewGuid();

                var seed = new List<Event>()
                {
                    new Event(
                        eventId1,
                        "Some title",
                        "Some description",
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        false,
                        new List<EventValue>()
                        {
                            new EventValue(Guid.NewGuid(), eventId1, "Breeze", false),
                            new EventValue(Guid.NewGuid(), eventId1, "Brady", false),
                        }),
                    new Event()
                    {
                        Id = eventId2,
                        Title = "Some title",
                        Description = "Some description",
                        StartDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(1),
                        IsFinished = true,
                        EventValues = new List<EventValue>()
                        {
                            new EventValue(Guid.NewGuid(), eventId2, "Montana", true),
                        }
                    }
                };

                return seed;
            }
        }
    }
}
