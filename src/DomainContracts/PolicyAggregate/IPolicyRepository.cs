using DomainContracts.Commons;
using DomainEntities.PolicyAggregate;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.PolicyAggregate
{
	public interface IPolicyRepository : IRepository<Policy>, IAsyncRepository<Policy>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<Policy> FindByIdAsync(int id);
	}
}
