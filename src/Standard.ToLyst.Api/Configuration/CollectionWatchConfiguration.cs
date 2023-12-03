using Standard.ToLyst.Model.Aggregates.Products;

namespace Standard.ToLyst.Api.Configuration
{
    public static class CollectionWatchConfiguration
	{
        public static void StartWatch(this WebApplication app)
		{
			var scope = app.Services.CreateScope();
			scope.ServiceProvider.GetService<IProductRepository>().WatchAsync();
		}
	}
}

