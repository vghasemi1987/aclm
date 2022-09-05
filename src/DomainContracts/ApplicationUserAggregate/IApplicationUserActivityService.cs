using DomainEntities.ApplicationUserAggregate;
using System.Threading.Tasks;

namespace DomainContracts.ApplicationUserAggregate
{
	public interface IApplicationUserActivityService
	{
		Task Save(ApplicationUserActivity model);
	}
}