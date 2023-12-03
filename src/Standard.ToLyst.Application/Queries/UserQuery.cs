using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Users;

namespace Standard.ToLyst.Application.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly IUserRepository _repository;

        public UserQuery(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<UserViewModel>> GetAsync(Request request)
        {
            var result = new Result<UserViewModel>(null);
            var user = await _repository.GetOneAsync(it => it.Id == request.ResourceId);

            if (user == null)
                return result.SetResult(ResultStatus.NotFound, "User not found.");

            return result.SetResult(new UserViewModel(user), ResultStatus.Success, null);
        }
    }
}

