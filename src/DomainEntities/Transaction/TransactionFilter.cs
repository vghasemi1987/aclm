namespace DomainEntities.Transaction
{
	public class TransactionFilter
	{
		public string SourcePan { get; set; }
		public string TargetPan { get; set; }
		public string BeginTransactionDate { get; set; }
		public string EndTransactionDate { get; set; }
		public long Amount { get; set; }
		public int? HeaderId { get; set; }
	}
}
