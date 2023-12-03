using System.Threading.Tasks;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Users;

namespace Standard.ToLyst.Model.Aggregates.Users
{
    public interface IUserQuery
	{
		Task<Result<UserViewModel>> GetAsync(Request request);
	}
}
