using System;
using System.Collections.Generic;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class Event
    {
        public Event(Guid id, string title, string description, DateTime startDate, DateTime dueDate, bool isFinished, IEnumerable<EventValue> eventValues)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            DueDate = dueDate;
            IsFinished = isFinished;
            EventValues = eventValues;
        }

        public Event()
        {
        }

        public Guid Id { get; set; }

        public string? Title { get; set; }
        
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsFinished { get; set; }

        public IEnumerable<EventValue>? EventValues { get; set; }
    }
}
