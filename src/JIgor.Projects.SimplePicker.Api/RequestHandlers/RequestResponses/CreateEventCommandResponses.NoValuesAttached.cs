namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class CreateEventCommandResponses
    {
        public readonly struct NoValuesAttached
        {
            public NoValuesAttached(string message)
            {
                Message = message;
            }

            public string Message { get; }
        }
    }
}