namespace JIgor.Projects.SimplePicker.Api.Dtos.Default
{
    public class EventValueDto
    {
        public EventValueDto(string value)
        {
            Value = value;
        }

        public EventValueDto()
        {
        }

        public string? Value { get; set; }
    }
}
