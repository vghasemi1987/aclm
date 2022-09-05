using DomainEntities.Commons;

namespace DomainEntities.DutyPositionAggregate
{
	public class DutyPosition : Entity<int>
	{
		public int Code { get; set; }
		public string Title { get; set; }
	}
}