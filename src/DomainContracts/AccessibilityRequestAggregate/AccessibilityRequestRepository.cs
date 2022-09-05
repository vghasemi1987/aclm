using DomainContracts.Commons;
using DomainEntities.AccessibilityRequestAggregate;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.AccessibilityRequestAggregate
{
	public interface IAccessibilityRequestRepository : IRepository<AccessibilityRequest>, IAsyncRepository<AccessibilityRequest>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<AccessibilityRequest> FindByIdAsync(int id);
	}
}
