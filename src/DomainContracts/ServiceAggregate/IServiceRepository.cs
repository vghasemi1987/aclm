using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.ServiceAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ServiceAggregate
{
	public interface IServiceRepository : IRepository<Service>, IAsyncRepository<Service>
	{
		DataSourceResult GetList(DataSourceRequest request);
		List<Service> GetList();
		Task<Service> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
