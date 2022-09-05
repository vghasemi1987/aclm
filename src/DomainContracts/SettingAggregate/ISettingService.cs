using DomainEntities.SettingAggregate;
using System.Threading.Tasks;

namespace DomainContracts.SettingAggregate
{
	public interface ISettingService
	{
		Task SaveEmailAsync(Setting model);
		Task SaveSmsAsync(Setting model);
		Task SaveAppAsync(Setting model);
	}
}