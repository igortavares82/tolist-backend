using System.Reflection;
using FluentValidation;
using MediatR;
using Standard.ToList.Api.Middlewares;
using Standard.ToList.Application.Queries;
using Standard.ToList.Infrastructure.Searchers;
using Standard.ToList.Application.Services;
using Standard.ToList.Application.Validators;
using Standard.ToList.Infrastructure.Repositories;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Logs;
using Standard.ToList.Model.Aggregates.Lysts;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Aggregates.Watchers;

namespace Standard.ToList.Api.Configuration
{
    public static class DIConfiguration
	{
		public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
		{	
			services.AddMediatR(configuration =>
			{
                configuration.RegisterServicesFromAssemblies(Assembly.Load("Standard.ToList.Application"));
				configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            })
			.AddValidatorsFromAssembly(Assembly.Load("Standard.ToList.Application"));

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

