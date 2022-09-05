using DomainEntities.TestOptionsAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.TestOptionsAggregate
{
	public interface ITestOptionService
	{
		Task<List<TestOptions>> GetByTableId(TestOptionsTable id);
	}
}
