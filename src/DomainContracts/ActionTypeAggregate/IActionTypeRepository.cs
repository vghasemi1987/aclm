using DomainContracts.Commons;
using DomainEntities.ActionTypeAggregate;
using DomainEntities.Commons;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.ActionTypeAggregate
{
	public interface IActionTypeRepository : IRepository<ActionType>, IAsyncRepository<ActionType>
	{
		DataSourceResult GetList(DataSourceRequest request);
		List<ActionType> GetList();
		Task<ActionType> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
