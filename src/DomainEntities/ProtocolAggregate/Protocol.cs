using DomainEntities.Commons;

namespace DomainEntities.ProtocolAggregate
{
	public class Protocol : Entity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
