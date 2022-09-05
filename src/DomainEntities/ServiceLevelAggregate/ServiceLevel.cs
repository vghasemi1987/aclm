using DomainEntities.Commons;

namespace DomainEntities.ServiceLevelAggregate
{
	public class ServiceLevel : Entity<int>
	{
		public string Title { get; set; }
	}
}
