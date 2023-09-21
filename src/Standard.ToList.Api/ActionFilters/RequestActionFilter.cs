using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToList.Api.Extensions;

namespace Standard.ToList.Api.ActionFilters
{
    public class RequestActionFilter : IActionFilter
	{
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers.ExtractToken();

            RequestExtension.ExtractRequest(context)
                            .ExtrectId(context)
                            .ExtractUserId(token)
                            .ExtractRole(token);
        }
    }
}

