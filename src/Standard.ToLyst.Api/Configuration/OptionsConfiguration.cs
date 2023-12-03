using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Api.Configuration
{
    public static class OptionsConfiguration
	{
		public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AppSettingOptions>(options => configuration.Bind(options));
			services.Configure<LoggerOptions>(options => configuration.Bind(options));
		}
	}
}
