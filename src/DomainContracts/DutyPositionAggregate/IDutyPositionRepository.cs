using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.DutyPositionAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.DutyPositionAggregate
{
	public interface IDutyPositionRepository : IRepository<DutyPosition>, IAsyncRepository<DutyPosition>
	{
		DataSourceResult GetList(DataSourceRequest request);
		List<DutyPosition> GetList();
		Task<DutyPosition> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();
	}
}
