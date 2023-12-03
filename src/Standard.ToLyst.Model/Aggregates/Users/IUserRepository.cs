using System.Threading.Tasks;

namespace Standard.ToLyst.Model.Aggregates.Users
{
    public interface IUserRepository : IRepository<User>
	{
		Task<bool> CanRegisterAsync(string email);
	}
}
