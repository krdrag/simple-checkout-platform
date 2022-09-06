using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SCP.Common.Constants;
using SCP.Common.ErrorHandling;
using SCP.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace SCP.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            this._next = next;
            this._logger = logger;
            this._env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ErrorResponse response = new ErrorResponse();

                switch (ex)
                {
                    case BaseException bEx:
                        response = new ErrorResponse
                        {
                            ErrorCode = bEx.ErrorCode,
                            Message = bEx.Message,
                            MessageTranslated = string.Empty,
                            Details = _env.IsDevelopment() ? (ex.StackTrace ?? string.Empty) : string.Empty
                        };
                        break;
                    case Exception:
                        response = new ErrorResponse
                        {
                            ErrorCode = GeneralConstants.ErrorCodes.GeneralError,
                            Message = ex.Message,
                            MessageTranslated = string.Empty,
                            Details = _env.IsDevelopment() ? (ex.StackTrace ?? string.Empty) : string.Empty
                        };
                        break;
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
