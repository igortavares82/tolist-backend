using System.Threading.Tasks;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Users;

namespace Standard.ToList.Model.Aggregates.Users
{
    public interface IUserQuery
	{
		Task<Result<UserViewModel>> GetAsync(Request request);
	}
}
