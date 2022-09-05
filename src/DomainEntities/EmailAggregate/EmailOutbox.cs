using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;

namespace DomainEntities.EmailAggregate
{
	public class EmailOutbox : Entity<int>
	{
		public EmailOutbox()
		{
			EntryDateTime = DateTimeOffset.Now;
			IsSent = false;
		}
		public DateTimeOffset EntryDateTime { get; private set; }
		public string Email { get; set; }
		public bool IsSent { get; set; }
		public string Message { get; set; }
		public string Subject { get; set; }
		public int? UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}