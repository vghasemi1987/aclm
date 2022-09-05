using DomainEntities.Commons;
using System.Collections.Generic;

namespace DomainEntities.ToDoTaskAggregate
{
	public class State : Entity<short>
	{
		public string Title { get; set; }
		public IList<ToDoTask> ToDoTasks { get; set; }
	}
}