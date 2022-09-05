using DomainContracts.Commons;
using DomainEntities.TopupChannelTransaction;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.TopupChannelTransactionAggregate
{
	public interface ITopupChannelTransactionHeaderRepository : IRepository<TopupChannelTransactionHeader>, IAsyncRepository<TopupChannelTransactionHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<TopupChannelTransactionHeader> FindByIdAsync(int id);
	}
}
