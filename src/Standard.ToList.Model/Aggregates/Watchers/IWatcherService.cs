using System.Threading.Tasks;
using Standard.ToList.Model.Aggregates.Configuration;

namespace Standard.ToList.Model.Aggregates.Watchers
{
    public interface IWatcherService
	{
        Task<Worker> SendMessagesAsync(Worker worker);
        Task<Worker> UpdateWatchersAsync(Worker worker);
    }
}

