using DomainContracts.Commons;
using DomainEntities.AccessTypeAggregate;
using DomainEntities.Commons;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.AccessTypeAggregate
{
	public interface IAccessTypeRepository : IRepository<AccessType>, IAsyncRepository<AccessType>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<AccessType> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
