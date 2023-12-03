using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Standard.ToLyst.Model.Common;

namespace Standard.ToLyst.Model.Helpers
{
    public static class HttpContextHelper
	{
		public static async Task WriteResult<TEntity>(this HttpContext httpContext, Result<TEntity> result, HttpStatusCode statusCode)
		{
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            httpContext.Response.StatusCode = (int)statusCode;

            if (statusCode != HttpStatusCode.NoContent)
            {
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        }
	}
}

