using System.Net;
using System.Net.Http;

namespace Funda.Infrastructure.Http.Exceptions
{
    public class HttpFailedRequestException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public HttpFailedRequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
