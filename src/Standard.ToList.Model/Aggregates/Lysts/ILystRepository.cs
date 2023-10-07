using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Standard.ToList.Model.Common;

namespace Standard.ToList.Model.Aggregates.Lists
{
	public interface ILystRepository : IRepository<Lyst>
	{
        Task<Lyst> GetOneAsync(string id);
        Task<Lyst> GetOneAsync(Expression<Func<Lyst,bool>> expression);
        Task<IEnumerable<Lyst>> GetAsync(string userId, string name, bool isDraft, bool isEnabled, Page page);
        Task UpdateAsync(Lyst lyst);
    }
}
