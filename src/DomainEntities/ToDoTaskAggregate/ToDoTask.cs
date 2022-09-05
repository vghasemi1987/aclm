using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using DomainEntities.NotificationAggregate;
using DomainEntities.SettingAggregate;
using System;
using System.Collections.Generic;

namespace DomainEntities.ToDoTaskAggregate
{
	public class ToDoTask : Entity<int>
	{
		public ToDoTask()
		{
			EntryDateTime = DateTime.Now;
		}
		public string Title { get; set; }
		public short? UsageTypeId { get; set; }
		public UsageType UsageType { get; set; }
		public DateTime? DueDateTime { get; set; }//زمان پایان کار
		public DateTime? CompletionDateTime { get; set; }
		public ApplicationUser AssignedToUser { get; set; }
		public int? AssignedToUserId { get; set; }
		public int CreatorUserId { get; set; }
		public ApplicationUser CreatorUser { get; set; }
		public string Description { get; set; }
		public short? PriorityId { get; set; }
		public Priority Priority { get; set; }
		public short? StateId { get; set; }
		public State State { get; set; }
		public DateTime EntryDateTime { get; private set; }
		public ICollection<NotificationItem> NotificationItems { get; set; }
	}
}
