using System;

namespace JIgor.Projects.SimplePicker.Api.Dtos
{
    public class CreateEventRequestDto
    {
        public CreateEventRequestDto(string title, string description, DateTime startDate, DateTime dueDate)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            DueDate = dueDate;
        }

        public CreateEventRequestDto()
        {
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
