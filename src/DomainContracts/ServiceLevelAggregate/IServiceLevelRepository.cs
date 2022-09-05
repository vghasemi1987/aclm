using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.ServiceLevelAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ServiceLevelAggregate
{
	public interface IServiceLevelRepository : IRepository<ServiceLevel>, IAsyncRepository<ServiceLevel>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<ServiceLevel> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
