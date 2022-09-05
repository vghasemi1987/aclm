using DomainEntities.Commons;

namespace DomainEntities.RouterAggregate
{
	public class Router : Entity<int>
	{
		public string Name { get; set; }
		public string AccessNo { get; set; }
	}
}