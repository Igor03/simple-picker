using System;
using System.Collections.Generic;

namespace JIgor.Projects.SimplePicker.Api.Dtos.Default
{
    public class EventDto
    {
        public EventDto(string title, string description, DateTime startDate, DateTime dueDate, bool isFinished, IEnumerable<EventValueDto> eventValues)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            DueDate = dueDate;
            IsFinished = isFinished;
            EventValues = eventValues;
        }

        public EventDto()
        {
        }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsFinished { get; set; }

        public IEnumerable<EventValueDto>? EventValues { get; set; }
    }
}
