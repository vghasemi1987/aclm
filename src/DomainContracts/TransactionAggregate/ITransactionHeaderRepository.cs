using DomainContracts.Commons;
using DomainEntities.Transaction;
using KendoHelper;
using System.Threading.Tasks;

namespace DomainContracts.TransactionAggregate
{
	public interface ITransactionHeaderRepository : IRepository<TransactionHeader>, IAsyncRepository<TransactionHeader>
	{
		DataSourceResult GetList(DataSourceRequest request);
		Task<TransactionHeader> FindByIdAsync(int id);
	}
}
