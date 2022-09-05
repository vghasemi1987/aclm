using DomainEntities.Commons;
using DomainEntities.ProtocolAggregate;

namespace DomainEntities.PolicyAggregate
{
	public class Policy : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int? ProtocolId { get; set; }
		public Protocol Protocol { get; set; }
	}
}