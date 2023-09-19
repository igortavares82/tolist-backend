using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Users
{
    public interface IUserRepository : IRepository<User>
	{
		Task<bool> CanRegisterAsync(string email);
	}
}
