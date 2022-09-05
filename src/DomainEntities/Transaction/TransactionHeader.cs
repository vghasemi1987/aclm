using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;
using System.Collections.Generic;

namespace DomainEntities.Transaction
{

	public class TransactionHeader : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime UploadDate { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }
		public ICollection<TransactionDetail> Details { get; set; }

	}
}
