using System.Threading.Tasks;
using Standard.ToLyst.Model.Aggregates.Configuration;

namespace Standard.ToLyst.Model.Aggregates.Watchers
{
    public interface IWatcherService
	{
        Task<Worker> SendMessagesAsync(Worker worker);
        Task<Worker> UpdateWatchersAsync(Worker worker);
    }
}

