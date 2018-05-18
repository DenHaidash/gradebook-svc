using System;
using System.Net;
using System.Threading.Tasks;
using GradeBook.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GradeBook.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                
                _logger.LogError(ex, "Application error");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ResourceAccessPermissionException _:
                    code = HttpStatusCode.Forbidden;
                    break;
                
                case GradebookException _:
                    code = HttpStatusCode.BadRequest;
                    break;
            }
            
            var result = JsonConvert.SerializeObject(new { Error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}