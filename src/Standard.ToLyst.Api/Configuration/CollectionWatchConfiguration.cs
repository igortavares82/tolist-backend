using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Api.Configuration
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

