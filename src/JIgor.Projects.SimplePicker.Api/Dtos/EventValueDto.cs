namespace JIgor.Projects.SimplePicker.Api.Dtos
{
    public partial class EventValueDto
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