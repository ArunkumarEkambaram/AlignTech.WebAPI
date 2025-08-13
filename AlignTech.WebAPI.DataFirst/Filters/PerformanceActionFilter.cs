using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AlignTech.WebAPI.DataFirst.Filters
{
    public class PerformanceActionFilter : IActionFilter
    {
        private readonly ILogger<PerformanceActionFilter> _logger;
        private Stopwatch? _stopwatch;

        public PerformanceActionFilter(ILogger<PerformanceActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            var methodName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Monitoring the performance of the method {methodName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch?.Stop();
            var elaspsedTime = _stopwatch?.ElapsedMilliseconds;
            var statusCode = context.HttpContext.Response.StatusCode;
            var methodName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Method name :{methodName} executed in {elaspsedTime}ms with Status Code :{statusCode}");
        }

    }
}
