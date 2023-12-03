using System.Threading.Tasks;
using Standard.ToList.Model.Common;
using Standard.ToList.Model.ViewModels.Configuration;

namespace Standard.ToList.Model.Aggregates.Configuration
{
    public interface IConfigurationQuery
    {
        Task<Result<ConfigurationViewModel>> GetAsync(ConfigurationRequest request);
    }
}
