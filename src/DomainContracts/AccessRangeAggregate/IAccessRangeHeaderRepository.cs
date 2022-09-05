using DomainContracts.Commons;
using DomainEntities.AccessRangeAggregate;
using DomainEntities.Commons;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.AccessRangeAggregate
{
	public interface IAccessRangeHeaderRepository : IRepository<AccessRangeHeader>, IAsyncRepository<AccessRangeHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<AccessRangeHeader> FindByIdAsync(int id);
		List<DropDownDto> GetDropDownList();
	}
}
