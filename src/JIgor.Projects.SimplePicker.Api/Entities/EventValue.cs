using System;

namespace JIgor.Projects.SimplePicker.Api.Entities
{
    public partial class EventValue
    {
        public EventValue(Guid id, Guid eventId, string value)
        {
            Id = id;
            EventId = eventId;
            Value = value;
        }

        public EventValue()
        {
        }

        public Guid Id { get; set; }

        public Guid EventId { get; set; }
        
        public string Value { get; set; }
    }
}
