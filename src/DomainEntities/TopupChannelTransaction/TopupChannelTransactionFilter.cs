namespace DomainEntities.TopupChannelTransaction
{
	public class TopupChannelTransactionFilter
	{
		public string OperatorName { get; set; }
		public string TransactionAmount { get; set; }
		public string CustomerAccountNumber { get; set; }
		public string FollowupCode { get; set; }
		public string TransactionStatus { get; set; }
		public string TransactionDate { get; set; }
		public int HeaderId { get; set; }
	}
}
