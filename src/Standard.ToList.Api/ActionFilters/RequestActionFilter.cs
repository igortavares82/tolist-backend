using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Api.ActionFilters
{
    public class RequestActionFilter : IActionFilter
	{
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var values = context.RouteData.Values;
            var request = GetRequest(context);

            if (values.TryGetValue("id", out var id))
            {
                request.ResourceId = id.ToString();
            }
        }

        private Request GetRequest(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var request);
            return request as Request ?? new Request();
        }
    }
}

