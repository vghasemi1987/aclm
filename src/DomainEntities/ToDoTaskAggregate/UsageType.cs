using DomainEntities.Commons;

namespace DomainEntities.ToDoTaskAggregate
{
	public class UsageType : Entity<short>
	{
		public string Title { get; set; }
	}
}