namespace DomainEntities.TopupAccountTransaction
{
	public class TopupAccountTransactionFilter
	{
		public string TransactionAmount { get; set; }
		public string CustomerAccountNumber { get; set; }
		public string FollowupCode { get; set; }
		public string TransactionStatus { get; set; }
		public string TransactionDate { get; set; }
		public string TransactionTime { get; set; }
		public int HeaderId { get; set; }
	}
}
