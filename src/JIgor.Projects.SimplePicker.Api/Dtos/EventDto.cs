using System;
using System.Collections.Generic;

namespace JIgor.Projects.SimplePicker.Api.Dtos
{
    public partial class EventDto
    {
        public EventDto(string? title, 
            string? description, 
            DateTime startDate, 
            DateTime dueDate, 
            IEnumerable<EventValueDto>? eventValues)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            DueDate = dueDate;
            EventValues = eventValues;
        }

        public EventDto()
        {
        }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public IEnumerable<EventValueDto>? EventValues { get; set; }
    }
}
