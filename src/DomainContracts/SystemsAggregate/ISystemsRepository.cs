using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.SystemsAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.SystemsAggregate
{
	public interface ISystemsRepository : IRepository<Systems>, IAsyncRepository<Systems>
	{
		DataSourceResult GetList(DataSourceRequest request);
		List<Systems> GetList();
		Systems FindByIdAsync(int id);
		List<DropDownDto> GetDropDownList();
		Task<List<DropDownDto>> GetDropDownListAsync();
		object GetSystemIpList();
	}
}
