using Newtonsoft.Json;

namespace JIgor.Projects.SimplePicker.Api.Commons.Classes
{
    internal class DefaultErrorResponse
    {
        public DefaultErrorResponse(string message, string exceptionStackTrace, int statusCode)
        {
            Message = message;
            ExceptionStackTrace = exceptionStackTrace;
            StatusCode = statusCode;
        }

        public string Message { get; set; }

        public string ExceptionStackTrace { get; set; }

        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}