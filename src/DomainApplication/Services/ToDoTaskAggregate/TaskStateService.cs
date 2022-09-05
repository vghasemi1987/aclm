using DomainContracts.Commons;
using DomainContracts.ToDoTaskAggregate;
using DomainEntities.ToDoTaskAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainApplication.Services.ToDoTaskAggregate
{
	public class TaskStateService : ITaskStateService
	{
		private readonly IAsyncRepository<State> _asyncTaskStateRepository;

		public TaskStateService(IAsyncRepository<State> asyncTaskStateRepository)
		{
			_asyncTaskStateRepository = asyncTaskStateRepository;
		}

		public async Task<IList<State>> GetAllAsync()
		{
			return await _asyncTaskStateRepository.ListAllAsync();
		}
	}
}
