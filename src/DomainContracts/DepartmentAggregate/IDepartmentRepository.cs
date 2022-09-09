using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.DepartmentAggregate;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.DepartmentAggregate
{
	public interface IDepartmentRepository : IRepository<Department>, IAsyncRepository<Department>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<Department> FindByIdAsync(int id);
		Task<List<DropDownDto>> GetDropDownList();

		Task<IList<Department>> GetDepartmentByName(string name);
	}
}
