using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToLyst.Application.Commands.InstanceCommands;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Api.Extensions
{
    public static class RequestExtension
    {
        public static Request ExtractId(this Request input, ActionExecutingContext context)
        {
            var values = context.RouteData.Values;

            if (values.TryGetValue("id", out var id))
            {
                input.ResourceId = id.ToString();
            }

            return input;
        }

		public static Request ExtractUserId(this Request input, string token)
		{
            if (!string.IsNullOrEmpty(token))
            {
                input.UserId = GetClaimValue(token, ClaimTypes.Sid);
            }

            return input;
        }

        public static Request ExtractRole(this Request input, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var role = Enum.Parse(typeof(RoleType), GetClaimValue(token, "role"));
                input.RoleType = (RoleType)role; 
            }

            return input;
        }

        public static Request ExtractInstanceId(this UpdateCommand input, ActionExecutingContext context)
        {
            var values = context.RouteData.Values;

            if (values.TryGetValue("instanceId", out var instanceId))
            {
                input.InstanceId = instanceId.ToString();
            }

            return input;
        }

        public static TRequest ExtractRequest<TRequest>(ActionExecutingContext context) where TRequest : new()
        {
            try
            {
                context.ActionArguments.TryGetValue("request", out var request);
                return (TRequest)request;
            }
            catch
            {
                return default(TRequest);
            }
        }

        private static string GetClaimValue(string token, string claimType)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.Claims.First(c => c.Type == claimType).Value;
        }

	}
}

