﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(TEntity[] entities);
        Task<IEnumerable<XEntity>> CreateAsync<XEntity>(XEntity[] entities);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression);
		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity entity);
		Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
	}
}

