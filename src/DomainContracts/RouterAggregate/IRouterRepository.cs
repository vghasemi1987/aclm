using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.RouterAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.RouterAggregate
{
	public interface IRouterRepository : IRepository<Router>, IAsyncRepository<Router>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<Router> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
