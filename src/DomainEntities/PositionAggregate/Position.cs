using DomainEntities.Commons;

namespace DomainEntities.PositionAggregate
{
	public class Position : Entity<int>
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}