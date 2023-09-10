using System;
using System.Linq;
using MongoDB.Driver;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Products;

namespace Standard.ToList.Infrastructure.Helpers
{
    public static class MongoDbHelper
	{
		public static FilterDefinition<Product> BuildProductFilter(string marketId, string[] names)
		{
            var builders = Builders<Product>.Filter;
            var filter = builders.Eq(it => it.Market.Id, marketId) &
                         builders.Eq(it => it.IsEnabled, true) &
                         builders.Or(builders.Regex(_it => _it.Name, $"(?i)(^{string.Join("|", names)}.*)"));

            return filter;
        }

        public static FilterDefinition<Product> BuildProductFilter(string marketId, int maxOutdated)
        {
            var outdate = DateTime.Now.AddDays(-maxOutdated);
            var builder = Builders<Product>.Filter;
            var filter = builder.Eq(it => it.Market.Id, marketId) &
                         builder.Lte(it => it.LastUpdate, outdate) |
                         builder.Eq(it => it.LastUpdate, null);

            return filter;
        }

        public static UpdateDefinition<TEntity> BuildUpdateDefinition<TEntity>(TEntity entity, params string[] excludedProperties)
        {
            var properties = typeof(TEntity).GetProperties().Where(it => !excludedProperties.Contains(it.Name));
            var builders = Builders<TEntity>.Update;
            var update = properties.Select(it => builders.Set(it.Name, it.GetValue(it))).Last();

            return update;
        }

        public static UpdateDefinition<Product> BuildUpdateDefinition(Product entity)
        { 
            var builders = Builders<Product>.Update;
            var update = builders.Set(it => it.Name, entity.Name)
                                 .Set(it => it.LastUpdate, entity.LastUpdate)
                                 .Set(it => it.Price, entity.Price);

            return update;
        }

        public static UpdateDefinition<Lyst> BuildUpdateDefinition(Lyst entity)
        {
            var builders = Builders<Lyst>.Update;
            var update = builders.Set(it => it.Name, entity.Name)
                                 .Set(it => it.LastUpdate, entity.LastUpdate)
                                 .Set(it => it.IsEnabled, entity.IsEnabled)
                                 .Set(it => it.Products, entity.Products)
                                 .Set(it => it.IsDraft, entity.IsDraft);

            return update;
        }
    }
}

