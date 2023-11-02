using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Standard.ToList.Model.Aggregates;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
	{
		protected MongoClient _client;
        protected AppSettingOptions _settings;

        protected IMongoCollection<TEntity> Collection => _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                                                                 .GetCollection<TEntity>(GetCollectionName(null));

        public Repository(IOptions<AppSettingOptions> settings)
		{
			if (settings == null)
				throw new ArgumentException("Database connection string cannot be null or empty.");

            _settings = settings.Value;
			_client = new MongoClient(_settings.ConnectionStrings.MongoDbConnection.ConnectionString);
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

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, int limit = 100)
        {
            var result = await Task.Run(() => Collection.Find(expression).Limit(limit));
            return result.ToEnumerable();
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            await Collection.ReplaceOneAsync(expression, entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(TEntity[] entities)
        {
            await Collection.InsertManyAsync(entities);
            return entities;
        }

        public async Task<IEnumerable<XEntity>> CreateAsync<XEntity>(XEntity[] entities)
        {
            await _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                         .GetCollection<XEntity>(GetCollectionName(typeof(XEntity)))
                         .InsertManyAsync(entities);

            return entities;
        }

        public async Task<IEnumerable<XEntity>> GetAsync<XEntity>(Expression<Func<XEntity, bool>> expression)
        {
            var result = await _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                                      .GetCollection<XEntity>(GetCollectionName(typeof(XEntity)))
                                      .FindAsync(expression);

            return result.ToEnumerable();
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = 10 };
            Parallel.ForEach(entities, options, it => Collection.ReplaceOneAsync(_it => _it.Id == it.Id, it));
        }

        protected string GetCollectionName(Type? type = null)
        {
            if (type == null)
                return typeof(TEntity).Name.ToLower();

            return type.Name.ToLower();
        }

        public async Task DeleteAsync<XEntity>(Expression<Func<XEntity, bool>> expression)
        {
            await _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                         .GetCollection<XEntity>(GetCollectionName(typeof(XEntity)))
                         .DeleteOneAsync(expression);
        }

        protected IMongoCollection<XEntity> GetCollection<XEntity>()
        {
            return _client.GetDatabase(_settings.ConnectionStrings.MongoDbConnection.DatabaseName)
                          .GetCollection<XEntity>(GetCollectionName(typeof(XEntity)));
        }

        public async Task<Result<IEnumerable<TEntity>>> GetAsync(Expression<Func<TEntity, bool>> expression, Page page)
        {
           return await GetAsync<TEntity>(expression, page);
        }

        public async Task<Result<IEnumerable<XEntity>>> GetAsync<XEntity>(Expression<Func<XEntity, bool>> expression, Page page)
        {
            if (page.Count == 0)
                page.Count = (int) await GetCollection<XEntity>().CountAsync(expression);
            
            var data = GetCollection<XEntity>().Find(expression).Skip(page.Skip).Limit(page.Limit).ToList();

            return new Result<IEnumerable<XEntity>>(data, ResultStatus.Success, page, null);
        }
    }
}

