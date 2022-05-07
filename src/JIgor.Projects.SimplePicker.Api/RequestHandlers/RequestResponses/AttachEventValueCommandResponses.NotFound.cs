namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class AttachEventValueCommandResponses
    {
        public readonly struct NotFound
        {
            public NotFound(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}