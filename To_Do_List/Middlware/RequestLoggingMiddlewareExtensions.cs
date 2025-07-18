namespace To_Do_List.Middlware
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
