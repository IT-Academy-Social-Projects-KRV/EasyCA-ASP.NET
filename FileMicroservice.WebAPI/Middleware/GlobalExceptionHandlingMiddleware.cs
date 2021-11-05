using System;
using System.Net;
using System.Threading.Tasks;
using FileMicroservice.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FileMicroservice.WebApi.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case ArgumentNullException _:
                    case ArgumentOutOfRangeException _:
                    case ArgumentException _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case RestException restException:
                        response.StatusCode = (int)restException.StatusCode;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var errorResponse = new
                {
                    message = GetMessage(ex),
                    statusCode = response.StatusCode
                };

                var result = JsonConvert.SerializeObject(errorResponse);

                await context.Response.WriteAsync(result);
            }
        }
        private string GetMessage(Exception ex)
        {
            var hasInnerException = ex.InnerException;

            if (hasInnerException != null)
            {
                return ex.InnerException.Message;
            }
            else
            {                    
                return ex.Message;
            }
        }
    }
}
