using DomainEntities.Commons;

namespace DomainEntities.ActionTypeAggregate
{
	public class ActionType : Entity<int>
	{
		public string Title { get; set; }
	}
}