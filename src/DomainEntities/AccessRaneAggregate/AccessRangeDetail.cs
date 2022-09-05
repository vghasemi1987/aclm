using DomainEntities.Commons;

namespace DomainEntities.AccessRangeAggregate
{
	public class AccessRangeDetail : Entity<int>
	{
		public string IpFrom { get; set; }
		public string IpTo { get; set; }
		public int AccessRangeHeaderId { get; set; }
		public AccessRangeHeader AccessRangeHeader { get; set; }
	}
}