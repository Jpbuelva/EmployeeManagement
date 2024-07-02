using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Infrastructure.Middleware
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

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            // Log the details of the incoming request
            var requestLog = new RequestLog
            {
                Method = context.Request.Method,
                Url = context.Request.Path,
                Timestamp = DateTime.UtcNow
            };

            dbContext.RequestLogs.Add(requestLog);
            await dbContext.SaveChangesAsync();

            // Call the next middleware in the pipeline
            await _next(context);
        }

    }
}
 