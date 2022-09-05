using DomainContracts.Commons;
using DomainEntities.AccessibilityLevelAggregate;
using DomainEntities.Commons;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.AccessibilityLevelAggregate
{
	public interface IAccessibilityLevelRepository : IRepository<AccessibilityLevel>, IAsyncRepository<AccessibilityLevel>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
