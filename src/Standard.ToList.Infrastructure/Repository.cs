using System;
using MongoDB.Driver;
using MongoDB.Bson;
using Standard.ToList.Model.Aggregates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.Extensions.Options;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected MongoClient _client;
        protected AppSettingOptions _settings;
        protected readonly string _collectionName;

        protected IMongoCollection<TEntity> Collection => _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                                                                 .GetCollection<TEntity>(_collectionName);

        public Repository(IOptions<AppSettingOptions> settings)
		{
			if (settings == null)
				throw new ArgumentException("Database connection string cannot be null or empty.");

            _settings = settings.Value;
			_client = new MongoClient(_settings.ConnectionStrings.MongoDbConnection.ConnectionString);
            _collectionName = nameof(TEntity);
		}

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            await Collection.DeleteOneAsync(expression);
        }

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await Collection.FindAsync<TEntity>(expression);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var result = await Collection.FindAsync(expression);
            return result.ToEnumerable();
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

