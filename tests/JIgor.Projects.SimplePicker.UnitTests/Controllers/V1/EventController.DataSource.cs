using System;
using System.Collections.Generic;
using JIgor.Projects.SimplePicker.Api.Dtos;
using JIgor.Projects.SimplePicker.Api.Entities;

namespace JIgor.Projects.SimplePicker.UnitTests.Controllers.V1
{
    public partial class EventControllerTests
    {
        public static class DataSource
        {
            public static IEnumerable<EventDto> FindEventsShouldReturnsExpectedResultOutput()
            {
                var eventId1 = Guid.NewGuid();
                var eventId2 = Guid.NewGuid();

                var output = new List<EventDto>()
                {
                    new EventDto(
                        "Some title",
                        "Some description",
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        new List<EventValueDto>()
                        {
                            new EventValueDto("Breeze"),
                            new EventValueDto("Brady"),
                        }),
                    new EventDto(
                        "Some title", 
                        "Some description", 
                        DateTime.Now, 
                        DateTime.Now.AddDays(1),
                        new List<EventValueDto>()
                        {
                            new EventValueDto()
                            {
                                Value = "Montana"
                            }
                        })
                };

                return output;
            }

            public static EventDto FindEventsShouldReturnNotFoundOutput()
            {
                var output = new EventDto(
                    "Some title",
                    "Some description",
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    new List<EventValueDto>()
                    {
                        new EventValueDto("Breeze"),
                        new EventValueDto("Brady"),
                    });

                return output;
            }

            public static EventDto CreateEventShouldReturnExpectedResultInput()
            {
                var input = new EventDto(
                    "Some title",
                    "Some description",
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    new List<EventValueDto>()
                    {
                        new EventValueDto("Breeze"),
                        new EventValueDto("Brady"),
                    });

                return input;
            }

            public static EventDto CreateEventShouldReturnBadRequestInput()
            {
                var input = new EventDto(
                    "Some title",
                    "Some description",
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    new List<EventValueDto>()
                    {
                    });

                return input;
            }

            public static IEnumerable<EventValueDto> GetAttachEventValueShouldReturnExpectedResultInput()
            {
                var output = new List<EventValueDto>()
                {
                    new EventValueDto("Breeze"),
                    new EventValueDto("Brady")
                };

                return output;
            }
        }
    }
}
