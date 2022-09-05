using DomainEntities.Commons;
using DomainEntities.SearchLogAggregate;
using System.Collections.Generic;

namespace DomainEntities.TopupChannelTransaction
{
	public class TopupChannelTransactionDetail : Entity<int>
	{
		public string FollowupCode { get; set; }
		public string CustomerAccountNumber { get; set; }
		public string FollowupCode2 { get; set; }
		public string Extra1 { get; set; }
		public string Extra2 { get; set; }
		public string ChannelType { get; set; }
		public string TransactionDate { get; set; }
		public string MobileNumber { get; set; }
		public string AmountText { get; set; }
		public string TransactionAmount { get; set; }
		public string TransactionStatus { get; set; }
		public string OperatorName { get; set; }
		public int HeaderId { get; set; }
		public TopupChannelTransactionHeader Header { get; set; }
		public ICollection<SearchLogTransactionDetail> SearchLogTransactionDetails { get; set; }
	}
}
