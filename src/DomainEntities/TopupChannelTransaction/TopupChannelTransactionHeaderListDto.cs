using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;

namespace DomainEntities.TopupChannelTransaction
{
	public class TopupChannelTransactionHeader : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime UploadDate { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
