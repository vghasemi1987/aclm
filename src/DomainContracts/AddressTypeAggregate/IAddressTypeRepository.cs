using DomainContracts.Commons;
using DomainEntities.AddressTypeAggregate;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.AddressTypeAggregate
{
	public interface IAddressTypeRepository : IRepository<AddressType>, IAsyncRepository<AddressType>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<AddressType> FindByIdAsync(int id);
	}
}
