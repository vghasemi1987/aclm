using DomainEntities.Commons;

namespace DomainEntities.ServiceGroupAggregate
{
	public class ServiceGroup : Entity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
