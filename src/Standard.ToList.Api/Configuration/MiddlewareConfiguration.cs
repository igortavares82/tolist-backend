using Standard.ToList.Api.Middlewares;

namespace Standard.ToList.Api.Configuration
{
    public static class MiddlewareConfiguration
	{
		public static void UseMiddleware(this WebApplication webApplication)
		{
			webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}

