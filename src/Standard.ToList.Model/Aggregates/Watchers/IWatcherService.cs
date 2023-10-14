using System.Threading.Tasks;

namespace Standard.ToList.Model.Aggregates.Watchers
{
    public interface IWatcherService
	{
        Task SendMessagesAsync();
    }
}

