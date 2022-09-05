using DomainEntities.ToDoTaskAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ToDoTaskAggregate
{
	public interface ITaskStateService
	{
		Task<IList<State>> GetAllAsync();
	}
}