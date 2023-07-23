using System;
using System.Reflection;
using Standard.ToList.Infrastructure.Repositories;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Application;
using Standard.ToList.Application.Commands.LystCommands;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Application.Queries;
using Standard.ToList.Model.Aggregates.Markets;

namespace Standard.ToList.Api.Configuration
{
	public static class DIConfiguration
	{
		public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
		{	
			services.AddMediatR(configuration =>
			{
                configuration.RegisterServicesFromAssemblies(Assembly.Load("Standard.ToList.Application"));
            });

			services.AddScoped<ILystRepository, LystRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductQuery, ProductQuery>();
            services.AddScoped<IMarketRepository, MarketRepository>();
        }
	}
}

