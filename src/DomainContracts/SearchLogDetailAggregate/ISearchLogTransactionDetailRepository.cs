using DomainContracts.Commons;
using DomainEntities.SearchLogAggregate;

namespace DomainContracts.ServiceAggregate
{
	public interface ISearchLogTransactionDetailRepository : IRepository<SearchLogTransactionDetail>, IAsyncRepository<SearchLogTransactionDetail>
	{
	}
}
