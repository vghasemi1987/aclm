using DomainEntities.SettingAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.SettingAggregate
{
	public interface IPriorityService
	{
		Task<IList<Priority>> GetAllAsync();
	}
}
