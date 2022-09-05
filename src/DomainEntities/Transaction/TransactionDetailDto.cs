namespace DomainEntities.Transaction
{
	public class TransactionDetailDto
	{

		public int Id { get; set; }
		public string SourcePan { get; set; }
		public string TargetPan { get; set; }
		private string _TransactionDateString;
		public string TransactionDateString
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_TransactionDateString))
				{
					return "0000-00-00";
				}
				else
					return _TransactionDateString;
			}
			set
			{
				_TransactionDateString = value;
			}
		}
		public string TransactionTime { get; set; }
		public string Tel { get; set; }
		private string _Amount;
		public string Amount
		{
			get { return _Amount; }
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
					_Amount = value;
				else
					_Amount = "";
			}
		}
		public string IpAddress { get; set; }
		public string Status { get; set; }
		public string RefNumber { get; set; }
		public string UserAgent { get; set; }
		public string Application { get; set; }
		public int? HeaderId { get; set; }
	}
}
