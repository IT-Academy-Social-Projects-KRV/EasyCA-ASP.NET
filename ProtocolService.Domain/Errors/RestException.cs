using System;
using System.Net;

namespace ProtocolService.Domain.Errors
{
    public class RestException: Exception
    {
        public HttpStatusCode StatusCode { get; }
        public RestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
