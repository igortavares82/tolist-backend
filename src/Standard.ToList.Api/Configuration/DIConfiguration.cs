using System.Reflection;
using FluentValidation;
using MediatR;
using Standard.ToList.Api.Middlewares;
using Standard.ToList.Application.Queries;
using Standard.ToList.Application.Services;
using Standard.ToList.Application.Validators;
using Standard.ToList.Infrastructure.Repositories;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Markets;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Aggregates.Users;

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

            services.AddScoped<ILystRepository, LystRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IMarketRepository, MarketRepository>();
			services.AddScoped<IMarketService, MarketService>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<ILystQuery, LystQuery>();
            services.AddScoped<IUserQuery, UserQuery>();

            services.AddSingleton<MarketFactory>();
			services.AddSingleton<TokenService>();

			services.AddTransient<ExceptionHandlingMiddleware>();
        }
	}
}

