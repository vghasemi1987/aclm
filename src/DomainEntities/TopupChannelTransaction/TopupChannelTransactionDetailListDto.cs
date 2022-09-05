namespace DomainEntities.TopupChannelTransaction
{
	public class TopupChannelTransactionDetailListDto
	{
		public int Id { get; set; }
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
	}
}
