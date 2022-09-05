using DomainContracts.ServiceAggregate;
using DomainEntities.SearchLogAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.SearchLogAggregate
{
	public class SearchLogTransactionDetailRepository : EfRepository<SearchLogTransactionDetail>, ISearchLogTransactionDetailRepository
	{
		private readonly DbSet<SearchLogDetail> _dbSet;
		public SearchLogTransactionDetailRepository(ServerAccessibilityMonitorContext dbContext) : base(dbContext)
		{
			_dbSet = dbContext.Set<SearchLogDetail>();
		}
	}
}
