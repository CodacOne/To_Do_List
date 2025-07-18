using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace To_Do_List.Middlware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            _logger.LogInformation("📥 Incoming Request: {method} {path}", request.Method, request.Path);

            try
            {
                await _next(context); // Run the next middleware or endpoint
                stopwatch.Stop();

                _logger.LogInformation("📤 Completed: {method} {path} | StatusCode: {statusCode} | Duration: {elapsed} ms",
                    request.Method,
                    request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex, "❌ Unhandled Exception occurred during request processing.");

                // Handle error response
                await HandleExceptionAsync(context, ex, stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, long elapsedMs)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                status = 500,
                message = "An unexpected error occurred.",
                error = exception.Message,
                durationMs = elapsedMs
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);

            _logger.LogInformation("📤 Error Response Sent: {json}", errorJson);

            await context.Response.WriteAsync(errorJson);
        }
    }
}
