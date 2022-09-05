using DomainEntities.Commons;
using DomainEntities.Transaction;

namespace DomainEntities.SearchLogAggregate
{
	public class SearchLogTransactionDetail : Entity<int>
	{
		public int SearchLogDetailId { get; set; }
		public SearchLogDetail SearchLogDetail { get; set; }
		public int TransactionDetailId { get; set; }
		public TransactionDetail TransactionDetail { get; set; }
	}
}
