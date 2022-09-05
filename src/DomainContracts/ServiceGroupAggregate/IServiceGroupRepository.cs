using DomainContracts.Commons;
using DomainEntities.ServiceGroupAggregate;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.ServiceGroupAggregate
{
	public interface IServiceGroupRepository : IRepository<ServiceGroup>, IAsyncRepository<ServiceGroup>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<ServiceGroup> FindByIdAsync(int id);
	}
}
