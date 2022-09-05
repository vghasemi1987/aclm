using DomainContracts.Commons;
using DomainEntities.TopupAccountTransaction;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.TopupAccountTransactionAggregate
{
	public interface ITopupAccountTransactionHeaderRepository : IRepository<TopupAccountTransactionHeader>, IAsyncRepository<TopupAccountTransactionHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<TopupAccountTransactionHeader> FindByIdAsync(int id);
	}
}
