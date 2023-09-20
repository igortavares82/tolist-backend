using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
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

            request.UserId = GetResourceId(context);
        }

        private Request GetRequest(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var request);
            return request as Request ?? new Request();
        }

        private string GetResourceId(ActionExecutingContext context)
        {
            string userId = string.Empty;

            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                string token = context.HttpContext.Request.Headers["Authorization"];
                token = token.Replace("Bearer ", string.Empty);

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                userId = jwt.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            }

            return userId;
        }
    }
}

