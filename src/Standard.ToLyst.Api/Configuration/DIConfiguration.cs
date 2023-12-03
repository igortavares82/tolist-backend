using System.Reflection;
using FluentValidation;
using MediatR;
using Standard.ToLyst.Api.Middlewares;
using Standard.ToLyst.Application.Queries;
using Standard.ToLyst.Infrastructure.Searchers;
using Standard.ToLyst.Application.Services;
using Standard.ToLyst.Application.Validators;
using Standard.ToLyst.Infrastructure.Repositories;
using Standard.ToLyst.Model.Aggregates;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Logs;
using Standard.ToLyst.Model.Aggregates.Lysts;
using Standard.ToLyst.Model.Aggregates.Markets;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Aggregates.Watchers;

namespace Standard.ToLyst.Api.Configuration
{
    public static class DIConfiguration
	{
		public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
		{	
			services.AddMediatR(configuration =>
			{
                configuration.RegisterServicesFromAssemblies(Assembly.Load("Standard.ToLyst.Application"));
				configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            })
			.AddValidatorsFromAssembly(Assembly.Load("Standard.ToLyst.Application"));

            services.ConfigureRepositories();
            services.ConfigureQueries();
            services.ConfigureServices();
            services.AddTransient<ExceptionHandlingMiddleware>();
        }

		private static void ConfigureRepositories(this IServiceCollection services)
		{
            services.AddScoped<ILystRepository, LystRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWatcherRepository, WatcherRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
        }

        private static void ConfigureQueries(this IServiceCollection services)
        {
            services.AddScoped<IWatcherQuery, WatcherQuery>();
            services.AddScoped<ILystQuery, LystQuery>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IInstanceQuery, InstanceQuery>();
            services.AddScoped<IConfigurationQuery, ConfigurationQuery>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IWatcherService, WatcherService>();
            services.AddScoped<IMarketService, MarketService>();
            services.AddSingleton<SearcherFactory>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<SmtpService>();
            services.AddSingleton<WorkerService>();
        }
    }
}

