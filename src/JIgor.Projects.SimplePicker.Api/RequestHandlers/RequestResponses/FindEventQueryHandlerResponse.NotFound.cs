namespace JIgor.Projects.SimplePicker.Api.RequestHandlers.RequestResponses
{
    public partial class FindEventQueryHandlerResponse
    {
        public struct NotFound
        {
            public NotFound(string message)
            {
                Message = message;
            }

            public string Message { get; set; }
        }
    }
}
