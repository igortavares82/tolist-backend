using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Standard.ToList.Infrastructure.Helpers;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
		public ProductRepository(IOptions<AppSettingOptions> settings, IMediator mediator) : base(settings, mediator)
		{
		}

        public async Task<IEnumerable<Product>> GetAsync(string marketId, string[] names)
        {
            var products = await base.Collection.FindAsync<Product>(MongoDbHelper.BuildProductFilter(marketId, names));
            return products.ToEnumerable();
        }

        public async Task<IEnumerable<Product>> GetAsync(string[] marketIds, string[] names, Page page, Order order)
        {
            var regex = new BsonRegularExpression(string.Format("([\\.]*{0}[\\.]*)", names[0]), "i");
            var definition = Builders<Product>.Filter;
            var filter = Builders<Product>.Filter.Empty;

            marketIds.ToList().ForEach(it => filter |= definition.Eq(_it => _it.Market.Id, it));
            var find = Collection.Find((filter) & definition.Regex(_it => _it.Name, BsonRegularExpression.Create(regex)));

            SortDefinition<Product> sort = Builders<Product>.Sort.Ascending(order.Field);

            if (order.Direction == -1)
                sort = Builders<Product>.Sort.Descending(order.Field);

            return find.Sort(sort)
                        .Skip(page.Skip)
                        .Limit(page.Limit)
                        .ToList();
        }

        public async Task<IEnumerable<Product>> GetAsync(string marketId, int maxOutdated, int limit)
        {         
            return await Task.Run(() => Collection.Find(MongoDbHelper.BuildProductFilter(marketId, maxOutdated)).Limit(limit).ToEnumerable());
        }

        public async Task<IEnumerable<MissingProduct>> GetMissingProductsAsync(MissingProduct[] missingProducts)
        {
            var definition = Builders<MissingProduct>.Filter;
            var filter = Builders<MissingProduct>.Filter.Empty;

            missingProducts.ToList().ForEach(it => filter |= definition.Eq(_it => _it.MarketId, it.MarketId) & 
                                                             definition.Eq(_it => _it.Name, it.Name));
            
            var _missingProducts = await GetCollection<MissingProduct>().FindAsync(filter);
            return _missingProducts.ToList();
        }

        public new async Task UpdateAsync(params Product[] products)
        {
            foreach (var product in products)
            {
                await base.Notificate(product as Entity);
                await base.Collection.ReplaceOneAsync(it => it.Id == product.Id, product);
            }
        }

        public new async Task CreateAsync(params Product[] products)
        {
            await Task.Run(() => base.Collection.InsertMany(products));
        }

        public async Task WatchAsync()
        {

        }
    }
}
