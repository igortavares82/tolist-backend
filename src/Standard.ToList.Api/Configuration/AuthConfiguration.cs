namespace Standard.ToList.Api.Configuration
{
    public static class AuthConfiguration
	{
		public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });
        }
	}
}

