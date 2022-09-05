using DomainContracts.Commons;
using DomainEntities.TestOptionsAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TestOptionsAggregate
{
	public interface ITestOptionRepository : IRepository<TestOptions>, IAsyncRepository<TestOptions>
	{
		Task<TestOptions> FindByIdAsync(short id);
		Task<List<TestOptions>> GetByTableId(TestOptionsTable id);
	}
}
