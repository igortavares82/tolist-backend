using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Standard.ToList.Infrastructure.Helpers;
using Standard.ToList.Model.Aggregates.Products;
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

        public async Task<IEnumerable<Product>> GetAsync(string marketId, int maxOutdated, int limit)
        {         
            return await Task.Run(() => Collection.Find(MongoDbHelper.BuildProductFilter(marketId, maxOutdated)).Limit(limit).ToEnumerable());
        }

        public async Task UpdateAsync(params Product[] products)
        {
            foreach (var product in products)
            {
                await base.Collection.ReplaceOneAsync(it => it.Id == product.Id, product);
                await base.Notificate(product.Notifications.ToArray());
            }
        }

        public async Task WatchAsync()
        {
            try
            {

                var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
                var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<BsonDocument>>()
                                .Match(change => change.OperationType == ChangeStreamOperationType.Drop);

                using var cursor = await Collection.WatchAsync(options);

                while (cursor.MoveNext() && cursor.Current.Count() == 0)
                {
                    var product = cursor.First().FullDocument;
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
