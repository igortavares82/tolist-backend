using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToList.Api.Extensions;
using Standard.ToList.Application.Commands.InstanceCommands;
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
            string token = context.HttpContext.Request.Headers.ExtractToken();

            RequestExtension.ExtractRequest<Request>(context)
                            .ExtractId(context)
                            .ExtractUserId(token)
                            .ExtractRole(token);

            RequestExtension.ExtractRequest<UpdateCommand>(context)?
                            .ExtractInstanceId(context);
        }
    }
}

