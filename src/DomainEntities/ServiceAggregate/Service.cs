using DomainEntities.Commons;
using DomainEntities.ProtocolAggregate;
using DomainEntities.ServiceLevelAggregate;

namespace DomainEntities.ServiceAggregate
{
	public class Service : Entity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int? Port { get; set; }
		public int? ProtocolId { get; set; }
		public Protocol Protocol { get; set; }
		public int? ServiceLevelId { get; set; }
		public ServiceLevel ServiceLevel { get; set; }
	}
}
