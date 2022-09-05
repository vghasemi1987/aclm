using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;

namespace DomainEntities.TopupAccountTransaction
{
	public class TopupAccountTransactionHeader : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime UploadDate { get; set; }
		public int UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
