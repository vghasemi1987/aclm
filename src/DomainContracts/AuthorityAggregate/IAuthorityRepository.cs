using DomainContracts.Commons;
using DomainEntities.AuthorityAggregate;
using DomainEntities.Commons;
using KendoHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.AuthorityAggregate
{
	public interface IAuthorityRepository : IRepository<Authority>, IAsyncRepository<Authority>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<Authority> FindByIdAsync(int id);
		List<DropDownDto> GetDropDownList();
	}
}
