using System.Threading.Tasks;
using Standard.ToLyst.Model.Common;
using Standard.ToLyst.Model.ViewModels.Configuration;

namespace Standard.ToLyst.Model.Aggregates.Configuration
{
    public interface IConfigurationQuery
    {
        Task<Result<ConfigurationViewModel>> GetAsync(ConfigurationRequest request);
    }
}
