namespace DomainEntities.Transaction
{
	public class TransactionDetailListDto
	{
		public int Id { get; set; }
		public int RecordStatus { get; set; }
		public string SourcePan { get; set; }
		public string TargetPan { get; set; }
		public string TransactionDate { get; set; }
		public string TransactionTime { get; set; }
		public string Tel { get; set; }
		public long Amount { get; set; }
		public string IpAddress { get; set; }
		public string LogDate { get; set; }
		public string Status { get; set; }
		public string RefNumber { get; set; }
		public string UserAgent { get; set; }
		public int HeaderId { get; set; }
	}
}
