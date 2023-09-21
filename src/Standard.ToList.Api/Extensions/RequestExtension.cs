using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Api.Extensions
{
    public static class RequestExtension
    {
        public static Request ExtrectId(this Request input, ActionExecutingContext context)
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

        public static Request ExtractRequest(ActionExecutingContext context)
        {
            context.ActionArguments.TryGetValue("request", out var request);
            return request as Request ?? new Request();
        }

        private static string GetClaimValue(string token, string claimType)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwt.Claims.First(c => c.Type == claimType).Value;
        }

	}
}

