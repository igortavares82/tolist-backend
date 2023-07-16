using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(string entityId);
		Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> UpdateAsync(TEntity entity);
		Task DeleteAsync(TEntity entity);
	}
}

