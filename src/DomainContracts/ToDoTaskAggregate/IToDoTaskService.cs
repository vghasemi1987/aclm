using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ToDoTaskAggregate
{
	public interface IToDoTaskService
	{
		Task DeleteAsync(int itemId, string fileLocation);
		Task DeleteAsync(IEnumerable<int> itemId, string fileLocation);
		Task BackgroundTaskNotificationAsync();
	}
}