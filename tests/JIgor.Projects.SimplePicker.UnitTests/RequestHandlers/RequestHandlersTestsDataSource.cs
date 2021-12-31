using System;
using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.UnitTests.RequestHandlers
{
    public static class RequestHandlersTestsDataSource
    {
        public static object[] GetFindEventsQueryHandlerShouldReturnExpectedResultData()
        {
            var eventId1 = Guid.NewGuid();
            var eventId2 = Guid.NewGuid();

            var eventEntitySeed = new List<Event>()
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

            var mappedEntitySeed = new List<EventDto>()
            {
                new EventDto()
                {
                    Title = "Some title",
                    Description = "Some description",
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(1),
                    EventValues = new List<EventValueDto>()
                    {
                        new EventValueDto("Breeze"),
                        new EventValueDto("Brady")
                    }
                },
                new EventDto()
                {
                    Title = "Some title",
                    Description = "Some description",
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(1),
                    EventValues = new List<EventValueDto>()
                    {
                        new EventValueDto("Montana")
                    }
                }
            };

            return new object[] {eventEntitySeed, mappedEntitySeed};
        }
    }
}
