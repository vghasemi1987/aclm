using DomainEntities.Commons;

namespace DomainEntities.AccessTypeAggregate
{
	public class AccessType : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}