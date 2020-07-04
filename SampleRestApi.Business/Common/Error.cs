using System.Net;

namespace SampleRestApi.Business.Common
{
    public class Error
    {
        public HttpStatusCode Code { get; }

        public string Message { get; set; }

        public Error(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
