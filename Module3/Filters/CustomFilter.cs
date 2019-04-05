using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Module2.Filters
{
    public class CustomFilter : IActionFilter
    {
        private readonly ILogger<CustomFilter> _logger;
        private readonly bool _logExecutionPlan;

        public CustomFilter(ILogger<CustomFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _logExecutionPlan = configuration.GetValue<bool>("LogActionExecutionPlan");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_logExecutionPlan)
                return;

            GetActionInfo(context, out string controller, out string action);
            _logger.LogInformation($"!!! [{controller}].[{action}] execution started at {DateTime.Now}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!_logExecutionPlan)
                return;

            GetActionInfo(context, out string controller, out string action);
            _logger.LogInformation($"!!! [{controller}].[{action}] execution finished at {DateTime.Now}");
        }

        private static void GetActionInfo(FilterContext context,
            out string controller,
            out string action)
        {
            controller = (string)context.RouteData.Values["Controller"];
            action = (string)context.RouteData.Values["Action"];
        }
    }
}
