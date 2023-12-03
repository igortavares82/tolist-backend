using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToLyst.Api.Extensions;
using Standard.ToLyst.Application.Commands.InstanceCommands;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Api.ActionFilters
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

