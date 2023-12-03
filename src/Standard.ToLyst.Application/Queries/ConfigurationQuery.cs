using System;
using System.Threading.Tasks;
using Standard.ToLyst.Application.Extensions;
using Standard.ToLyst.Model;
using Standard.ToLyst.Model.Aggregates.Configuration;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.Constants;
using Standard.ToLyst.Model.ViewModels.Configuration;

namespace Standard.ToLyst.Application.Queries
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
