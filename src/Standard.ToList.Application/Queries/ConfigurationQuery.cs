using System;
using System.Threading.Tasks;
using Standard.ToList.Application.Extensions;
using Standard.ToList.Model;
using Standard.ToList.Model.Aggregates.Configuration;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.Constants;
using Standard.ToList.Model.ViewModels.Configuration;

namespace Standard.ToList.Application.Queries
{
    public class ConfigurationQuery : IConfigurationQuery
    {
        private readonly IConfigurationRepository _repository;

        public ConfigurationQuery(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ConfigurationViewModel>> GetAsync(ConfigurationRequest request)
        {
            var result = new Result<ConfigurationViewModel>(null);
            var configuration = await _repository.GetOneAsync(it => it.Id != null);
            
            if (configuration == null)
                return result.SetResult(null, ResultStatus.NotFound,  Messages.NotFound.SetMessageValues("Configuration"));

            return result.SetResult(new ConfigurationViewModel(configuration, request), ResultStatus.Success, null);
        }
    }
}
