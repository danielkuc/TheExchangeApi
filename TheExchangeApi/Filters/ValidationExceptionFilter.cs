using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TheExchangeApi.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        private readonly ApiBehaviorOptions _apiBehaviorOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidationExceptionFilter(
            IOptions<ApiBehaviorOptions> apiBehaviorOptions,
            IHttpContextAccessor httpContextAccessor)
        {
            _apiBehaviorOptions = apiBehaviorOptions?.Value ?? throw new ArgumentNullException(nameof(apiBehaviorOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is not ValidationException validationException)
            {
                return;
            }

            context.ExceptionHandled = true;

            var errors = validationException.Data;

            //var errors = validationException.Errors
            //    .GroupBy(validationFailure => validationFailure.PropertyName)
            //    .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(validationFailure => validationFailure.ErrorMessage)
            //    .ToArray());
            var problemDetails = new ValidationProblemDetails((IDictionary<string, string[]>)errors)
            {
                Status = 400
            };

            if (_apiBehaviorOptions.ClientErrorMapping.TryGetValue(problemDetails.Status.Value, out var clientErrorData))
            {
                problemDetails.Type ??= clientErrorData.Link;
                problemDetails.Title ??= clientErrorData.Title;
            }

            var traceId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier;

            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }
    }
}
