using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Standard.ToLyst.Model.Aggregates.Lists;
using Standard.ToLyst.Model.Aggregates.Products;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Infrastructure.Repositories
{
    public class LystRepository : Repository<Lyst>, ILystRepository
	{
		public LystRepository(IOptions<AppSettingOptions> settings) : base(settings)
		{
		}

		public async Task<Lyst> GetOneAsync(string id)
		{
			var result = base.Collection
							 .Aggregate()
							 .Lookup(GetCollectionName(typeof(Product)), "Products._id", "_id", "Products")
							 .As<Lyst>();
		
			return await result.Match(it => it.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Lyst> GetOneAsync(Expression<Func<Lyst, bool>> expression)
		{
            var result = base.Collection
                             .Aggregate()
                             .Lookup(GetCollectionName(typeof(Product)), "Products._id", "_id", "Products")
                             .As<Lyst>();

            return await result.Match(expression)
							   .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Lyst>> GetAsync(string userId, string name, bool isDraft, bool isEnabled, Page page)
        {
            

            var result = Collection.Find(it => it.UserId == userId &&
											   it.Name.Contains(name) &&
											   it.IsDraft == isDraft &&
											   it.IsEnabled == isEnabled)
									.Skip(page.Skip)
									.Limit(page.Limit);

            return await result.ToListAsync();
        }

        public async Task UpdateAsync(Lyst lyst)
        {
            await base.Collection.ReplaceOneAsync(it => it.Id == lyst.Id, lyst);
        }
    }
}
