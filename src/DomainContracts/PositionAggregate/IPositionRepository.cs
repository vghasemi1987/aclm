using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.PositionAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.PositionAggregate
{
	public interface IPositionRepository : IRepository<Position>, IAsyncRepository<Position>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<Position> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
