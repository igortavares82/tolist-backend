using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Standard.ToList.Model.Aggregates.Lists;
using Standard.ToList.Model.Aggregates.Products;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
    public class LystRepository : Repository<Lyst>, ILystRepository
	{
		public LystRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}

		public async Task<Lyst> GetAsync(string id)
		{

			var result = base.Collection
							 .Aggregate()
							 .Lookup(GetCollectionName(typeof(Product)), "Products._id", "_id", "Products")
							 .As<Lyst>();
		
			return await result.Match(it => it.Id == id).FirstOrDefaultAsync();
		}

        public async Task UpdateAsync(Lyst lyst)
        {
            await base.Collection.ReplaceOneAsync(it => it.Id == lyst.Id, lyst);
        }
    }
}
