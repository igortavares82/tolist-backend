using System;
using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Lists
{
	public interface ILystRepository : IRepository<Lyst>
	{
        Task<Lyst> GetAsync(string id);
    }
}
