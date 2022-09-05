using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.ProtocolAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ProtocolAggregate
{
	public interface IProtocolRepository : IRepository<Protocol>, IAsyncRepository<Protocol>
	{
		DataSourceResult GetList(DataSourceRequest request);
		List<Protocol> GetList();
		Task<Protocol> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
