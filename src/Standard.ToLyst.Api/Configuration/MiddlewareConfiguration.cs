using Standard.ToLyst.Api.Middlewares;

namespace Standard.ToLyst.Api.Configuration
{
    public static class MiddlewareConfiguration
	{
		public static void UseMiddleware(this WebApplication webApplication)
		{
			webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}

