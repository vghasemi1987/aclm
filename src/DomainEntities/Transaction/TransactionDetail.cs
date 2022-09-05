using DomainEntities.Commons;
using DomainEntities.SearchLogAggregate;
using System;
using System.Collections.Generic;

namespace DomainEntities.Transaction
{
	public class TransactionDetail : Entity<int>
	{
		public string SourcePan { get; set; }
		public string TargetPan { get; set; }
		public string SourcePanOrgianl { get; set; }
		public string TargetPanOrgianl { get; set; }
		public DateTime? TransactionDate { get; set; }
		public string TransactionDateString { get; set; }
		public string TransactionTime { get; set; }
		public string Tel { get; set; }
		public long? Amount { get; set; }
		public string IpAddress { get; set; }
		public DateTime? LogDate { get; set; }
		public string Status { get; set; }
		public string RefNumber { get; set; }
		public string UserAgent { get; set; }
		public string Application { get; set; }
		public int? HeaderId { get; set; }
		public TransactionHeader Header { get; set; }
		public ICollection<SearchLogTransactionDetail> SearchLogTransactionDetails { get; set; }


		public TransactionDetail()
		{
			Header = new TransactionHeader();

		}
	}
}
