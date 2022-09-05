using DomainContracts.Commons;
using DomainContracts.SettingAggregate;
using DomainEntities.SettingAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainApplication.Services.SettingAggregate
{
	public class PriorityService : IPriorityService
	{
		private readonly IAsyncRepository<Priority> _asyncPriorityRepository;

		public PriorityService(IAsyncRepository<Priority> asyncPriorityRepository)
		{
			_asyncPriorityRepository = asyncPriorityRepository;
		}

		public async Task<IList<Priority>> GetAllAsync()
		{
			return await _asyncPriorityRepository.ListAllAsync();
		}
	}
}
