using System;

namespace DomainEntities.ToDoTaskAggregate
{
	public class ToDoTaskListDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime? DueDateTime { get; set; }//زمان پایان کار
		public string CreatorUser { get; set; }
		public string Priority { get; set; }
		public string State { get; set; }
		public DateTime EntryDateTime { get; set; }
	}
}
