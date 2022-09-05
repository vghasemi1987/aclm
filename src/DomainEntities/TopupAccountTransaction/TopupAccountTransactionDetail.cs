using DomainEntities.Commons;
using DomainEntities.SearchLogAggregate;
using System.Collections.Generic;

namespace DomainEntities.TopupAccountTransaction
{
	public class TopupAccountTransactionDetail : Entity<int>
	{
		public string TransactionDate { get; set; }
		public string TransactionTime { get; set; }
		public string TransactionType { get; set; }
		public string TransactionStatus { get; set; }
		public string TransactionAmountText { get; set; }
		public string TransactionAmount { get; set; }
		public string RefrenceCode { get; set; }
		public string Extra1 { get; set; }
		public string Extra2 { get; set; }
		public string Extra3 { get; set; }
		public string Extra4 { get; set; }
		public string BranchCode { get; set; }
		public string FollowupCode2 { get; set; }
		public string AccountNumber { get; set; }
		public string CustomerAccountNumber { get; set; }
		public string FollowupCode { get; set; }

		public int HeaderId { get; set; }
		public TopupAccountTransactionHeader Header { get; set; }
		public ICollection<SearchLogTransactionDetail> SearchLogTransactionDetails { get; set; }
	}
}
